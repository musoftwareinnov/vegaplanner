import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Statistics } from '../models/statistics';

@Injectable()
export class StatisticsService {

  private readonly statestatusEndpoint = '/api/planningappstatistics';
  constructor(private http: HttpClient) { }

  getPlanningStatistics()  {
    return this.http.get<Statistics>(this.statestatusEndpoint)
  }
}