import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Statistics } from '../models/statistics';
import { JwtHeader } from '../shared/utils/jwt.header';

@Injectable()
export class StatisticsService {

  private readonly statestatusEndpoint = '/api/planningappstatistics';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private jwtHeader:JwtHeader) { 
    //getJwtHeader injects user service to get Web Token and create header
    this.httpHeaders = jwtHeader.getJwtHeader();
  }

  getPlanningStatistics()  {
    return this.http.get<Statistics>(this.statestatusEndpoint,{ headers: this.httpHeaders })
  }
}