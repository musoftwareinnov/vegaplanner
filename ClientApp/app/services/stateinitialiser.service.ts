import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StateInitialiser } from '../models/stateinitialiser';

@Injectable()
export class StateInitialiserService {

  private readonly stateInitialiserEndpoint = '/api/stateinitialisers';
  constructor(private http: HttpClient) { }

  getStateInitialiserList(filter:any)  {
    return this.http.get<any>(this.stateInitialiserEndpoint + '?' + this.toQueryString(filter))
      //.map(res => res.json());
  }

  getStateInitialiser(id: number)  {
    return this.http.get<StateInitialiser>(this.stateInitialiserEndpoint + '/' + id)
      //.map(res => res.json());
  }

  create(stateInitialiser:any) {
    return this.http.post(this.stateInitialiserEndpoint, stateInitialiser)
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