import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class DrawingService {

  constructor(private http: Http) { }

  upload(planningAppId = 0, drawing: any) {
    var formData = new FormData();
    formData.append('file', drawing )
    
    return this.http.post(`/api/planningapps/${planningAppId}/drawings`, formData )
      .map(res => res.json());
  }

  getDrawings(planningAppId: number) {
    return this.http.get(`/api/planningapps/${planningAppId}/drawings`)
      .map(res => res.json());
  }
}