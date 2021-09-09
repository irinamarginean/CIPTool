from flask import Flask, request
import pyodbc
import pandas as pd
import json
import sqlalchemy
from sqlalchemy import ForeignKey, Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker, relationship
import urllib
from sentence_transformers import SentenceTransformer, util
from flask_cors import CORS

model = SentenceTransformer('paraphrase-distilroberta-base-v1')
api = Flask(__name__)
CORS(api)
api.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///inquestion.db'
params = urllib.parse.quote_plus('Driver=SQL Server;'
                                     'Server=CLJ-C-0005V;'
                                     'Database=CIPTool;'
                                     'Trusted_Connection=yes;')
engine = sqlalchemy.create_engine("mssql+pyodbc:///?odbc_connect=%s" % params)
metadata = sqlalchemy.MetaData(bind=engine)


def connectToDatabase():
    connection = pyodbc.connect('Driver={SQL Server};'
                                'Server=CLJ-C-005V;'
                                'Database=CIPTool;'
                                'Trusted_Connection=yes;')
    cursor = connection.cursor()
    cursor.exceute('SELECT * FROM CIPTool.Ideas')


@api.route('/')
def hello_world():
    return 'Hello World!'


@api.route('/all-ideas')
def all_ideas():
    connection = pyodbc.connect('Driver=SQL Server;'
                                'Server=CLJ-C-0005V;'
                                'Database=CIPTool;'
                                'Trusted_Connection=yes;')
    cursor = connection.cursor()

    sql_query = pd.read_sql_query('SELECT * FROM [CIPTool].[dbo].[Ideas]', connection)

    print(sql_query)
    print(type(sql_query))

    data = pd.DataFrame.to_dict(sql_query, orient='records')
    json_data = json.dumps(data)

    return json_data


Base = declarative_base()


class Idea(Base):
    __tablename__ = 'Ideas'
    Id = Column(String, primary_key=True)
    Title = Column(String)
    Context = Column(String)
    Target = Column(String)
    Description = Column(String)
    Categories = relationship('Category', secondary='CategoryIdeaEntity', back_populates="Ideas")


class Category(Base):
    __tablename__ = 'Categories'
    Id = Column(Integer, primary_key=True)
    Text = Column(String)
    Ideas = relationship(Idea, secondary='CategoryIdeaEntity', back_populates="Categories")


class CategoryIdeaEntity(Base):
    __tablename__ = 'CategoryIdeaEntity'
    CategoriesId = Column(Integer, ForeignKey('Categories.Id'), primary_key=True)
    IdeasId = Column(String, ForeignKey('Ideas.Id'), primary_key=True)


@api.route('/all-ideas/sql-alchemy')
def all_ideas_sql():
    ideas_table = sqlalchemy.Table('Ideas', metadata, autoload=True)
    ideas_query = sqlalchemy.select([
        ideas_table.columns.Title,
        ideas_table.columns.Context,
        ideas_table.columns.Target,
        ideas_table.columns.Description
    ])

    connection = engine.connect()
    results = connection.execute(ideas_query).fetchall()

    json_data = json.dumps([(dict(row.items())) for row in results])

    return json_data


@api.route('/all-ideas/sql-2')
def all_ideas_sql2():
    connection = engine.connect()
    Session = sessionmaker(bind=connection)
    session = Session()

    query = session.query(Idea)
    results = connection.execute(query.statement).fetchall()
    all_results = [(dict(row.items())) for row in results]

    for idea in all_results:
        current_categories_query = session.query(Category) \
            .select_from(Category) \
            .join(CategoryIdeaEntity) \
            .filter(CategoryIdeaEntity.IdeasId == idea['Id'])
        current_categories = list(dict(connection.execute(current_categories_query.statement).fetchall()).values())
        idea['Categories'] = current_categories

    json_data = json.dumps(all_results)

    return json_data


@api.route('/all-categories')
def all_categories():
    connection = engine.connect()
    Session = sessionmaker(bind=connection)
    session = Session()
    query = session.query(Category)
    results = connection.execute(query.statement).fetchall()
    json_data = json.dumps([(dict(row.items())) for row in results])

    return json_data


@api.route('/similarity', methods=['POST'])
def get_idea_similarities():
    new_idea = request.get_json()

    connection = engine.connect()
    Session = sessionmaker(bind=connection)
    session = Session()

    query = session.query(Idea)
    results = connection.execute(query.statement).fetchall()
    all_results = [(dict(row.items())) for row in results]

    if all_results.count() == 0:
        return []

    for idea in all_results:
        current_categories_query = session.query(Category) \
            .select_from(Category) \
            .join(CategoryIdeaEntity) \
            .filter(CategoryIdeaEntity.IdeasId == idea['Id'])
        current_categories = list(dict(connection.execute(current_categories_query.statement).fetchall()).values())
        idea['Categories'] = current_categories

    json_data = json.dumps(all_results)

    ideas = pd.read_json(json_data)

    ideas_ids = ideas['Id'].values.tolist()
    ideas_titles = ideas['Title'].values.tolist()
    ideas_context = ideas['Context'].values.tolist()
    ideas_target = ideas['Target'].values.tolist()
    ideas_description = ideas['Description'].values.tolist()
    ideas_categories_list = ideas['Categories'].values.tolist()
    ideas_categories = [" ".join(categories) for categories in ideas_categories_list]

    new_idea_title_list = [new_idea['title']] * len(ideas)
    new_idea_context_list = [new_idea['context']] * len(ideas)
    new_idea_target_list = [new_idea['target']] * len(ideas)
    new_idea_description_list = [new_idea['description']] * len(ideas)
    new_idea_categories_list = [" ".join(new_idea['categories'])] * len(ideas)

    # Compute embedding for both lists (title)
    embeddings_new_title = model.encode(new_idea_title_list, convert_to_tensor=True)
    embeddings_title = model.encode(ideas_titles, convert_to_tensor=True)

    # Compute embedding for both lists (current context)
    embeddings_new_context = model.encode(new_idea_context_list, convert_to_tensor=True)
    embeddings_context = model.encode(ideas_context, convert_to_tensor=True)

    # Compute embedding for both lists (target)
    embeddings_new_target = model.encode(new_idea_target_list, convert_to_tensor=True)
    embeddings_target = model.encode(ideas_target, convert_to_tensor=True)

    # Compute embedding for both lists (description)
    embeddings_new_description = model.encode(new_idea_description_list, convert_to_tensor=True)
    embeddings_description = model.encode(ideas_description, convert_to_tensor=True)

    # Compute embedding for both lists (categories)
    embeddings_new_categories = model.encode(new_idea_categories_list, convert_to_tensor=True)
    embeddings_categories = model.encode(ideas_categories, convert_to_tensor=True)

    # Compute cosine-similarities
    cosine_scores_title = util.pytorch_cos_sim(embeddings_new_title, embeddings_title)
    cosine_scores2_context = util.pytorch_cos_sim(embeddings_new_context, embeddings_context)
    cosine_scores_target = util.pytorch_cos_sim(embeddings_new_target, embeddings_target)
    cosine_scores_description = util.pytorch_cos_sim(embeddings_new_description, embeddings_description)
    cosine_scores_categories = util.pytorch_cos_sim(embeddings_new_categories, embeddings_categories)

    similarities = []

    for i in range(len(ideas)):
        cosine_title = cosine_scores_title[i][i]
        cosine_context = cosine_scores2_context[i][i]
        cosine_target = cosine_scores_target[i][i]
        cosine_description = cosine_scores_description[i][i]
        cosine_categories = cosine_scores_categories[i][i]

        similarity_percentage_title = (1 - cosine_title) / 2 if cosine_title < 0 else cosine_title
        similarity_percentage_context = (1 - cosine_context) / 2 if cosine_context < 0 else cosine_context
        similarity_percentage_target = (1 - cosine_target) / 2 if cosine_target < 0 else cosine_target
        similarity_percentage_description = (
                                                    1 - cosine_description) / 2 if cosine_description < 0 else cosine_description
        similarity_percentage_categories = (1 - cosine_categories) / 2 if cosine_categories < 0 else cosine_categories

        similarity_percentage = float(similarity_percentage_title) * 0.15 + float(
            similarity_percentage_context) * 0.15 + \
                                float(similarity_percentage_target) * 0.25 + float(
            similarity_percentage_description) * 0.3 + \
                                float(similarity_percentage_categories) * 0.15

        idea = {
            "id": ideas_ids[i],
            "title": ideas_titles[i],
            "context": ideas_context[i],
            "target": ideas_target[i],
            "description": ideas_description[i],
            "similarity": float(similarity_percentage) * 100
        }
        similarities.append(idea)

    relevant_similarities = [d for d in similarities if d["similarity"] > 40]
    ideas_json = json.dumps(relevant_similarities)

    return ideas_json


if __name__ == '__main__':
    api.run(host='localhost', port=8000)
