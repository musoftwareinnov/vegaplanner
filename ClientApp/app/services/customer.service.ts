import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

@Injectable()
export class CustomerService {

  private readonly customersEndpoint = '/api/customers';
  constructor(private http: Http) { }

  getCustomers(filter:any)  {
    return this.http.get(this.customersEndpoint  + '?' + this.toQueryString(filter))
      .map(res => res.json());
  }

  getCustomer(id: number)  {
    return this.http.get(this.customersEndpoint + '/' + id)
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