import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHeader } from '../shared/utils/jwt.header';
import { UserService } from '../shared/services/user.service';

@Injectable()
export class StateStatusService {

  private readonly statestatusEndpoint = '/api/statestatus';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private userService:UserService) { }

  getStateStatuses(status: string)  {
    return this.http.get<any>(this.statestatusEndpoint + "?statusName=" + status,{ headers: this.userService.getUwt() });
  }
}