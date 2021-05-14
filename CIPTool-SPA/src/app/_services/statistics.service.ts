import { IdeaStatisticsDto } from './../_models/ideaStatisticsDto';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

  baseUrl = environment.apiUrl + 'statistics/';
  // baseUrl = environment.iisUrl + 'statistics/';

  constructor(private httpClient: HttpClient) { }

  getIdeaStatistics(): Observable<IdeaStatisticsDto> {
    return this.httpClient.get<IdeaStatisticsDto>(this.baseUrl + 'ideas');
  }
}
