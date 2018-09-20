import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { StateInitialiser } from '../models/stateinitialiser';
import { JwtHeader } from '../shared/utils/jwt.header';

@Injectable()
export class StateInitialiserService {

  private readonly stateInitialiserEndpoint = '/api/stateinitialisers';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private jwtHeader:JwtHeader) { 
    //getJwtHeader injects user service to get Web Token and create header
    this.httpHeaders = jwtHeader.getJwtHeader();
  }

  getStateInitialiserList(filter:any)  {
    return this.http.get<any>(this.stateInitialiserEndpoint + '?' + this.toQueryString(filter),{ headers: this.httpHeaders })
      //.map(res => res.json());
  }

  getStateInitialiser(id: number)  {
    return this.http.get<StateInitialiser>(this.stateInitialiserEndpoint + '/' + id, { headers: this.httpHeaders })
      //.map(res => res.json());
  }

  create(stateInitialiser:any) {
    return this.http.post(this.stateInitialiserEndpoint, stateInitialiser, { headers: this.httpHeaders })
     // .map(res => res.json());
  }

  toQueryString(obj:any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if( value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value))
    }
}}