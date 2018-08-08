import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class StateStatusService {

  private readonly statestatusEndpoint = '/api/statestatus';
  constructor(private http: Http) { }

  getStateStatuses(id: number)  {
    return this.http.get(this.statestatusEndpoint)
      .map(res => res.json());
  }
}