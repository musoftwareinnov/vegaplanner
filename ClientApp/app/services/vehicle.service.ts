import { Vehicle, SaveVehicle } from '../models/vehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {

  private readonly vehiclesEndpoint = '/api/vehicles';
  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any>('/api/makes')
      //.map(res => res.json());
  }

  getModels() {
    return this.http.get<any>('/api/models')
      //.map(res => res.json());
  }

  getFeatures() {
    return this.http.get<any>('api/features')
      //.map(res => res.json());
  }

  create(vehicle:any) {
    vehicle.id=0;
    return this.http.post(this.vehiclesEndpoint, vehicle)
      //.map(res => res.json());
  }

  update(vehicle: SaveVehicle) {
    return this.http.put<Vehicle>(this.vehiclesEndpoint + '/' + vehicle.id, vehicle)
      //.map(res => res.json());
  }

  delete(id:any) {
    return this.http.delete(this.vehiclesEndpoint + '/' + id)
      //.map(res => res.json());
  }

  getVehicle(id:any) {
    return this.http.get(this.vehiclesEndpoint + '/' + id)
      //.map(res => res.json());
  }

  getVehicles(filter:any) {
    return this.http.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter))
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
