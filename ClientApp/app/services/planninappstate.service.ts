import { PlanningAppState } from '../models/planningappstate';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PlanningAppStateService {

  private readonly planningappstateEndpoint = '/api/planningappstate';
  constructor(private http: HttpClient) { }

  getPlanningAppState(id:any) {
    return this.http.get<PlanningAppState>(this.planningappstateEndpoint + '/' + id)
  }

  updatePlanningAppState(planningappstate:PlanningAppState) {
    console.warn("state = " + planningappstate)
    return this.http.put(this.planningappstateEndpoint + '/' + planningappstate.id, planningappstate)
  }
}