import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from '../models/customer';
import { HttpJwtService } from '../shared/services/httpJwt.service';

@Injectable()
export class CustomerService {

  private readonly customersEndpoint = '/api/customers';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private httpJwtService:HttpJwtService) {}

  getCustomers(filter:any)  {
    return this.httpJwtService.get<any>(this.customersEndpoint  + '?' + this.toQueryString(filter))
  }

  getCustomer(id: number)  {
    return this.httpJwtService.get<Customer>(this.customersEndpoint + '/' + id)
  }

  create(customer:any) {
    customer.id=0;
    return this.httpJwtService.post<Customer>(this.customersEndpoint, customer)
  }

  update(customer:any) {
    customer.planningApps = null; //Ignore planning applications - TODO create api with no app options
    return this.httpJwtService.put<Customer>(this.customersEndpoint + '/' + customer.id, customer)
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