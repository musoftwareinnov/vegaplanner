import { CustomerService } from './../../services/customer.service';
import { PlanningAppService } from '../../services/planningapp.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { Customer } from '../../models/customer';
import { StateStatusService } from '../../services/statestatus.service';
import { StateStatus } from '../../models/statestatus';

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
  
  stateStatuses: StateStatus[] = [];

  constructor(
  private route: ActivatedRoute,
  private router: Router,
  private PlanningAppService: PlanningAppService,
  private StateStatusService: StateStatusService,
  private CustomerService: CustomerService) { 

    route.params.subscribe(p => { this.query.customerId = +p['id'] || 0})
  }



  ngOnInit() {

    //Add status list drop down (may bring from server)
    this.loadStatuses()
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

  private loadStatuses() {
    this.StateStatusService.getStateStatuses("All")
      .subscribe(result => this.stateStatuses = result);
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

  onStateFilterChange() {
    console.warn("state = " + this.query.planningAppType);
    this.populatePlanningAppSummary();
  }
}





