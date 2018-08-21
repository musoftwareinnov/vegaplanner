import { PlanningApp, ChangePlanningAppState, PlanningAppGenerator } from '../models/planningapp';
import { PlanningAppSummary } from '../models/planningappsummary';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { StateAction } from '../constants';

@Injectable()
export class PlanningAppService {

  private readonly planningappsEndpoint = '/api/planningapps';
  constructor(private http: HttpClient) { }

  getPlanningAppSummary(filter:any) {
    return this.http.get<any>(this.planningappsEndpoint + '?' + this.toQueryString(filter))
      //.map(res => res.json());
  }

  getPlanningApp(id:any) {
    return this.http.get<PlanningApp>(this.planningappsEndpoint + '/' + id)
      //.map(res => res.json());
  }

  nextState(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Next;  //move to next state
    console.warn(changePlanningAppState);
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
      //.map(res => res.json());
  }

  terminate(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = StateAction.Terminate;  //move to next state
    console.warn(changePlanningAppState);
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
      //.map(res => res.json());
  }
  
  generatePlanningApp(planningAppGenerator:PlanningAppGenerator) {
    console.warn(planningAppGenerator);
    return this.http.post<PlanningApp>(this.planningappsEndpoint, planningAppGenerator)
      //.map(res => res.json());
  }

  toQueryString(obj:any) {
      var parts = [];
      for (var property in obj) {
        var value = obj[property];
        if( value != null && value != undefined)
          parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value))
      }

      return parts.join('&');
  }
}