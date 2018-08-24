import { Component, OnInit, NgModule } from '@angular/core';
import { CustomerService } from '../../services/customer.service';


@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})

export class CustomerListComponent implements OnInit {

  favoriteSeason: string = "";
  seasons: string[] = ['Winter', 'Spring', 'Summer', 'Autumn'];


  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    searchCriteria: "",
    sortBy:""
  };

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.populateCustomers();
  }

  onSearchChange(searchValue : string ) {;
    this.query.searchCriteria = searchValue;
    this.customerService.getCustomers(this.query)
    .subscribe(result => this.queryResult = result);
  }

  private populateCustomers() {
    this.customerService.getCustomers(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onPageChange(page:any) {
    this.query.page = page; 
    this.populateCustomers();
  }

  onFilterChange() {
    this.query.page = 1; 
    this.populateCustomers();
  }
}
