import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../models/customer';

@Injectable()
export class CustomerService {

  private readonly customersEndpoint = '/api/customers';
  constructor(private http: HttpClient) { }

  getCustomers(filter:any)  {
    console.warn("Filter " + this.toQueryString(filter));
    return this.http.get<any>(this.customersEndpoint  + '?' + this.toQueryString(filter))
      //.map(res => res.json());
  }

  getCustomer(id: number)  {
    return this.http.get<Customer>(this.customersEndpoint + '/' + id)
      //.map(res => res.json());
  }

  create(customer:any) {
    customer.id=0;
    return this.http.post(this.customersEndpoint, customer)
      //.map(res => res.json());
  }

  update(customer:any) {
    customer.planningApps = null; //Ignore planning applications - TODO create api with no app options
    return this.http.put(this.customersEndpoint + '/' + customer.id, customer)
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