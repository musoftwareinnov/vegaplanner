import { PlanningAppState } from '../models/planningappstate';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { JwtHeader } from '../shared/utils/jwt.header';

@Injectable()
export class PlanningAppStateService {

  private httpHeaders = new HttpHeaders;
  
  private readonly planningappstateEndpoint = '/api/planningappstate';
  constructor(private http: HttpClient, private jwtHeader:JwtHeader) { 
    //getJwtHeader injects user service to get Web Token and create header
    this.httpHeaders = jwtHeader.getJwtHeader();
  }
  getPlanningAppState(id:any) {
    return this.http.get<PlanningAppState>(this.planningappstateEndpoint + '/' + id, { headers: this.httpHeaders })
  }

  updatePlanningAppState(planningappstate:PlanningAppState) {
    console.warn("state = " + planningappstate)
    return this.http.put(this.planningappstateEndpoint + '/' + planningappstate.id, planningappstate, { headers: this.httpHeaders })
  }
}