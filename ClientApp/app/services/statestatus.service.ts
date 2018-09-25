import { StateStatus } from './../models/statestatus';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from '../shared/services/user.service';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class StateStatusService {

  private readonly statestatusEndpoint = '/api/statestatus';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) { }

  getStateStatuses(status: string)  {
    return this.httpJwtService.get<any>(this.statestatusEndpoint + "?statusName=" + status);
  }
}