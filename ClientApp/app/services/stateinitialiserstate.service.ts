import { StateInitialiserState } from './../models/stateinitialiserstate';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class StateInitialiserStateService {

  private readonly stateInitialiserstateEndpoint = '/api/stateinitialiserstates';
  constructor(private http: Http) { }

  getStateInitialiserState(id: number)  {
    return this.http.get(this.stateInitialiserstateEndpoint + '/' + id)
      .map(res => res.json());
  }

  update(stateInitialiserState: StateInitialiserState) {
    return this.http.put(this.stateInitialiserstateEndpoint + '/' + stateInitialiserState.id, stateInitialiserState)
      .map(res => res.json());
  }

  create(stateInitialiserState: StateInitialiserState) {
    return this.http.post(this.stateInitialiserstateEndpoint,stateInitialiserState)
      .map(res => res.json());
  }
}