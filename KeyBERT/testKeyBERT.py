from keybert import KeyBERT

doc = """
         Supervised learning is the machine learning task of learning a function that
         maps an input to an output based on example input-output pairs.[1] It infers a
         function from labeled training data consisting of a set of training examples.[2]
         In supervised learning, each example is a pair consisting of an input object
         (typically a vector) and a desired output value (also called the supervisory signal). 
         A supervised learning algorithm analyzes the training data and produces an inferred function, 
         which can be used for mapping new examples. An optimal scenario will allow for the 
         algorithm to correctly determine the class labels for unseen instances. This requires 
         the learning algorithm to generalize from the training data to unseen situations in a 
         'reasonable' way (see inductive bias).
      """
doc_one = "\n\nI am sure some bashers of Pens fans are pretty confused about the lack\nof " \
          "any kind of posts about the recent Pens massacre of the Devils. Actually,\nI am  " \
          "bit puzzled too and a bit relieved. However, I am going to put an end\nto non-PIttsburghers' " \
          "relief with a bit of praise for the Pens. Man, they\nare killing those Devils worse than I thought. " \
          "Jagr just showed you why\nhe is much better than his regular season stats. " \
          "He is also a lot\nfo fun to watch in the playoffs. Bowman should let JAgr have " \
          "a lot of\nfun in the next couple of games since the Pens are going to beat the " \
          "pulp out of Jersey anyway. I was very disappointed not to see the Islanders lose " \
          "the final\nregular season game.          PENS RULE!!!\n\n"

doc_two = "\n[stuff deleted]\n\nOk, here's the solution to your problem.  " \
          "Move to Canada.  Yesterday I was able\nto watch FOUR games...the NJ-PITT " \
          "at 1:00 on ABC, LA-CAL at 3:00 (CBC), \nBUFF-BOS at 7:00 (TSN and FOX), " \
          "and MON-QUE at 7:30 (CBC).  I think that if\neach series goes its max I " \
          "could be watching hockey playoffs for 40-some odd\nconsecutive nights " \
          "(I haven't counted so that's a pure guess).\n\nI have two tv's in my house, " \
          "and I set them up side-by-side to watch MON-QUE\nand keep an eye on " \
          "BOS-BUFF at the same time.  I did the same for the two\nafternoon games." \
          "\n\nBtw, those ABC commentaters were great!  I was quite impressed; they " \
          "seemed\nto know that their audience wasn't likely to be well-schooled in " \
          "hockey lore\nand they did an excellent job.  They were quite impartial also, IMO.\n\n"

model = KeyBERT('distilbert-base-nli-mean-tokens')
keywords = model.extract_keywords(doc)
key_phrases = model.extract_keywords(doc, keyphrase_ngram_range=(1, 2), stop_words=None)

print(keywords)
print(key_phrases)

keywords_one = model.extract_keywords(doc_one)
key_phrases_one = model.extract_keywords(doc_one, keyphrase_ngram_range=(1, 2), stop_words=None)

print(keywords_one)
print(key_phrases_one)

keywords_two = model.extract_keywords(doc_two)
key_phrases_two = model.extract_keywords(doc_two, keyphrase_ngram_range=(1, 2), stop_words=None)

print(keywords_two)
print(key_phrases_two)
