import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from '../models/customer';
import { JwtHeader } from '../shared/utils/jwt.header';

@Injectable()
export class CustomerService {

  private readonly customersEndpoint = '/api/customers';
  private httpHeaders = new HttpHeaders;
  
  constructor(private http: HttpClient, private jwtHeader:JwtHeader) { 
    //getJwtHeader injects user service to get Web Token and create header
    this.httpHeaders = jwtHeader.getJwtHeader();
  }
  getCustomers(filter:any)  {
    console.warn("Filter " + this.toQueryString(filter));
    return this.http.get<any>(this.customersEndpoint  + '?' + this.toQueryString(filter), { headers: this.httpHeaders })
  }

  getCustomer(id: number)  {
    return this.http.get<Customer>(this.customersEndpoint + '/' + id, { headers: this.httpHeaders })
  }

  create(customer:any) {
    customer.id=0;
    return this.http.post(this.customersEndpoint, customer, { headers: this.httpHeaders })
  }

  update(customer:any) {
    customer.planningApps = null; //Ignore planning applications - TODO create api with no app options
    return this.http.put(this.customersEndpoint + '/' + customer.id, customer, { headers: this.httpHeaders })
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