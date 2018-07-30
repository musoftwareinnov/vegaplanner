import { PlanningApp, ChangePlanningAppState, PlanningAppGenerator } from '../models/planningapp';
import { PlanningAppSummary } from '../models/planningappsummary';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PlanningAppService {

  private readonly planningappsEndpoint = '/api/planningapps';
  constructor(private http: Http) { }

  getPlanningAppSummary(filter:any) {
    return this.http.get(this.planningappsEndpoint + '?' + this.toQueryString(filter))
      .map(res => res.json());
  }

  getPlanningApp(id:any) {
    console.warn("plamnning App id:" + id);
    return this.http.get(this.planningappsEndpoint + '/' + id)
      .map(res => res.json());
  }

  nextState(changePlanningAppState: ChangePlanningAppState) {
    changePlanningAppState.method = 1;  //move to next state
    console.warn(changePlanningAppState);
    return this.http.put(this.planningappsEndpoint + '/' + changePlanningAppState.id, changePlanningAppState)
      .map(res => res.json());
  }
  
  generatePlanningApp(planningAppGenerator:PlanningAppGenerator) {
    console.warn(planningAppGenerator);
    return this.http.post(this.planningappsEndpoint, planningAppGenerator)
      .map(res => res.json());
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