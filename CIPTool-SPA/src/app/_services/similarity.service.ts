import { IdeaSimilarityDto } from './../_models/ideaSimilarityDto';
import { AddIdeaSimilarityDto } from './../_models/addIdeaSimilarityDto';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SimilarityService {

  baseUrl = environment.mlApiUrl;

  constructor(private httpClient: HttpClient) { }

  addIdea(idea: AddIdeaSimilarityDto): Observable<IdeaSimilarityDto[]> {
    return this.httpClient.post<IdeaSimilarityDto[]>(this.baseUrl + 'similarity', idea);
  }
}
