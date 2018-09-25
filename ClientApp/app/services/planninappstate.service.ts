import { PlanningAppState } from '../models/planningappstate';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class PlanningAppStateService {

  private httpHeaders = new HttpHeaders;
  
  private readonly planningappstateEndpoint = '/api/planningappstate';
  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) { 
  }
  getPlanningAppState(id:any) {
    return this.httpJwtService.get<PlanningAppState>(this.planningappstateEndpoint + '/' + id)
  }

  updatePlanningAppState(planningappstate:PlanningAppState) {
    console.info("state = " + planningappstate)
    return this.httpJwtService.put<PlanningAppState>(this.planningappstateEndpoint + '/' + planningappstate.id, planningappstate)
  }
}