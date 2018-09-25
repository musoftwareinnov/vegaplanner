import { StateInitialiserState } from './../models/stateinitialiserstate';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class StateInitialiserStateService {

  private readonly stateInitialiserstateEndpoint = '/api/stateinitialiserstates';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) { }

  getStateInitialiserState(id: number)  {
    return this.httpJwtService.get<StateInitialiserState>(this.stateInitialiserstateEndpoint + '/' + id)
  }

  update(stateInitialiserState: StateInitialiserState) {
    return this.httpJwtService.put<StateInitialiserState>(this.stateInitialiserstateEndpoint + '/' + stateInitialiserState.id, stateInitialiserState)
  }

  create(stateInitialiserState: StateInitialiserState) {
    return this.httpJwtService.post<StateInitialiserState>(this.stateInitialiserstateEndpoint,stateInitialiserState)
  }

  delete(id:any) {
    return this.httpJwtService.delete(this.stateInitialiserstateEndpoint + '/' + id)
  }
}