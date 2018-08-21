import { StateInitialiserState } from './../models/stateinitialiserstate';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StateInitialiserStateService {

  private readonly stateInitialiserstateEndpoint = '/api/stateinitialiserstates';
  constructor(private http: HttpClient) { }

  getStateInitialiserState(id: number)  {
    return this.http.get<StateInitialiserState>(this.stateInitialiserstateEndpoint + '/' + id)
      //.map(res => res.json());
  }

  update(stateInitialiserState: StateInitialiserState) {
    return this.http.put(this.stateInitialiserstateEndpoint + '/' + stateInitialiserState.id, stateInitialiserState)
      //.map(res => res.json());
  }

  create(stateInitialiserState: StateInitialiserState) {
    return this.http.post(this.stateInitialiserstateEndpoint,stateInitialiserState)
      //.map(res => res.json());
  }

  delete(id:any) {
    return this.http.delete(this.stateInitialiserstateEndpoint + '/' + id)
      //.map(res => res.json());
  }
}