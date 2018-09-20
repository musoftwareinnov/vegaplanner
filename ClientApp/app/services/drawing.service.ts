import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHeader } from '../shared/utils/jwt.header';

@Injectable()
export class DrawingService {

  private httpHeaders = new HttpHeaders;

  constructor(private http: HttpClient, private jwtHeader:JwtHeader) { 
    //getJwtHeader injects user service to get Web Token and create header
    this.httpHeaders = jwtHeader.getJwtHeader();
  }
  upload(planningAppId = 0, drawing: any) {
    var formData = new FormData();
    formData.append('file', drawing )
    
    return this.http.post(`/api/planningapps/${planningAppId}/drawings`, formData  )
      //.map(res => res.json());
  }

  getDrawings(planningAppId: number) {
    return this.http.get<any>(`/api/planningapps/${planningAppId}/drawings`, { headers: this.httpHeaders })
      //.map(res => res.json());
  }
}