import { StateInitialiserState } from './../models/stateinitialiserstate';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHeader } from '../shared/utils/jwt.header';

@Injectable()
export class StateInitialiserStateService {

  private readonly stateInitialiserstateEndpoint = '/api/stateinitialiserstates';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private jwtHeader:JwtHeader) { 
    //getJwtHeader injects user service to get Web Token and create header
    this.httpHeaders = jwtHeader.getJwtHeader();
  }

  getStateInitialiserState(id: number)  {
    return this.http.get<StateInitialiserState>(this.stateInitialiserstateEndpoint + '/' + id,{ headers: this.httpHeaders })
  }

  update(stateInitialiserState: StateInitialiserState) {
    return this.http.put(this.stateInitialiserstateEndpoint + '/' + stateInitialiserState.id, stateInitialiserState,{ headers: this.httpHeaders })
  }

  create(stateInitialiserState: StateInitialiserState) {
    return this.http.post(this.stateInitialiserstateEndpoint,stateInitialiserState,{ headers: this.httpHeaders })
  }

  delete(id:any) {
    return this.http.delete(this.stateInitialiserstateEndpoint + '/' + id,{ headers: this.httpHeaders })
  }
}