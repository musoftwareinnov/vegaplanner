import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { StateInitialiser } from '../models/stateinitialiser';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class StateInitialiserService {

  private readonly stateInitialiserEndpoint = '/api/stateinitialisers';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) { 
  }

  getStateInitialiserList(filter:any)  {
    return this.httpJwtService.get<any>(this.stateInitialiserEndpoint + '?' + this.toQueryString(filter))
  }

  getStateInitialiser(id: number)  {
    return this.httpJwtService.get<StateInitialiser>(this.stateInitialiserEndpoint + '/' + id)
  }

  create(stateInitialiser:any) {
    return this.httpJwtService.post<any>(this.stateInitialiserEndpoint, stateInitialiser)
  }

  toQueryString(obj:any) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if( value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value))
    }
}}