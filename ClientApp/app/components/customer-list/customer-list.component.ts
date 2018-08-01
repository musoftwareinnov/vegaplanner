import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.populateCustomers();
  }

  private populateCustomers() {
    this.customerService.getCustomers(this.query)
      .subscribe(result => this.queryResult = result);
  }

}
