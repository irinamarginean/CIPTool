import { LeaderResponse } from './../_models/leaderResponse';
import { IdeaDetails } from './../_models/ideaDetails';
import { IdeaSummary } from './../_models/ideaSummary';
import { AddIdeaInfoDto } from './../_models/addIdeaInfoDto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Idea } from '../_models/idea';
import { LeaderResponseOverview } from '../_models/leaderResponseOverview';

@Injectable({
  providedIn: 'root'
})
export class IdeaService {

  baseUrl = environment.apiUrl + 'ideas/';
  // baseUrl = environment.iisUrl + 'ideas/';

  constructor(private httpClient: HttpClient) { }

  addIdea(id: string, idea: Idea): Observable<Idea> {
    return this.httpClient.post<Idea>(this.baseUrl + 'add/' +  id, idea);
  }

  getIdeasByUsername(username: string): Observable<IdeaSummary[]> {
    return this.httpClient.get<IdeaSummary[]>(this.baseUrl + 'by-username/' + username);
  }

  getAllIdeas(): Observable<IdeaSummary[]> {
    return this.httpClient.get<IdeaSummary[]>(this.baseUrl + 'all');
  }

  getLeaderIdeasByUsername(username: string): Observable<LeaderResponseOverview> {
    return this.httpClient.get<LeaderResponseOverview>(this.baseUrl + 'responses-overview/' + username);
  }

  getAddIdeaInfo(): Observable<AddIdeaInfoDto> {
    return this.httpClient.get<AddIdeaInfoDto>(this.baseUrl + 'add/load');
  }

  getIdeaById(id: string): Observable<IdeaDetails> {
    return this.httpClient.get<IdeaDetails>(this.baseUrl + 'details/' + id);
  }

  addLeaderResponse(ideaId: string, leaderResponse: LeaderResponse): Observable<LeaderResponse> {
    return this.httpClient.put<LeaderResponse>(this.baseUrl + 'leader-response/' + ideaId, leaderResponse);
  }

  confirmImplementation(ideaId: string): Observable<any> {
    return this.httpClient.put(this.baseUrl + ideaId + '/confirm-implementation', null);
  }

  downloadFile(ideaId: string, fileId: string) {
    return this.httpClient.get<Blob>(this.baseUrl + 'download-file/byId?ideaId=' + ideaId + '&fileId=' + fileId,
                                    { observe: 'response', responseType: 'blob' as 'json' });
  }

  removeFile(ideaId: string, fileId: string) {
    return this.httpClient.delete(this.baseUrl + 'delete-file/byFilename?ideaId=' + ideaId + '&filename=' + fileId);
  }
}
