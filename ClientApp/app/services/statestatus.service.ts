import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StateStatusService {

  private readonly statestatusEndpoint = '/api/statestatus';
  constructor(private http: HttpClient) { }

  getStateStatuses(id: number)  {
    return this.http.get<any>(this.statestatusEndpoint)
  }
}