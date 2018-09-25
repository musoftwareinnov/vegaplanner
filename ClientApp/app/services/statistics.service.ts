import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Statistics } from '../models/statistics';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class StatisticsService {

  private readonly statestatusEndpoint = '/api/planningappstatistics';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) { }

  getPlanningStatistics()  {
    return this.httpJwtService.get<Statistics>(this.statestatusEndpoint)
  }
}