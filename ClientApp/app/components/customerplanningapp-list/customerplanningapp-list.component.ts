import { CustomerService } from './../../services/customer.service';
import { PlanningAppService } from '../../services/planningapp.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { Customer } from '../../models/customer';

@Component({
  selector: 'app-customerplanningapp-list',
  templateUrl: './customerplanningapp-list.component.html',
  styleUrls: ['./customerplanningapp-list.component.css']
})
export class CustomerPlanningAppListComponent implements OnInit {

  //Query Results
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    customerId: 0,
    planningAppType: "All"    //Get all applications for customer
  };
  interval: any = {};
  statusSelected: string = "";
  planningStatus: string[] = [];
  customer: any = {};


  constructor(
  private route: ActivatedRoute,
  private router: Router,
  private PlanningAppService: PlanningAppService,
  private CustomerService: CustomerService) { 

    route.params.subscribe(p => { this.query.customerId = +p['id'] || 0})
  }

  ngOnInit() {

    //Add status list drop down (may bring from server)
    this.planningStatus.push('All');
    this.planningStatus.push('InProgress');
    this.planningStatus.push('Complete');
    this.planningStatus.push('Archived');
    this.planningStatus.push('Terminated');

    this.populatePlanningAppSummary();
    this.populateCustomer();
    this.refreshData();
    this.interval = setInterval(() => { 
        this.refreshData(); 
    }, 5000);
  }

  refreshData() {
    this.populatePlanningAppSummary();
  }

  private populatePlanningAppSummary() {
    this.PlanningAppService.getPlanningAppSummary(this.query)
      .subscribe(result => this.queryResult = result);
  }

  private populateCustomer() {
    this.CustomerService.getCustomer(this.query.customerId)
      .subscribe(result => this.customer = result);
  }

  onPageChange(page:any) {
    this.query.page = page; 
    this.populatePlanningAppSummary();
  }

  onFilterChange() {
    this.query.page = 1; 
    this.query.planningAppType = this.statusSelected;
    
    this.populatePlanningAppSummary();
  }
}





