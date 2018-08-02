import { PlanningAppService } from '../../services/planningapp.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-customerplanningapp-list',
  templateUrl: './customerplanningapp-list.component.html',
  styleUrls: ['./customerplanningapp-list.component.css']
})
export class CustomerPlanningAppListComponent implements OnInit {
  //private customerId = 0;
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    customerId: 0
  };
  interval: any = {};

  constructor(
  private route: ActivatedRoute,
  private router: Router,
  private PlanningAppService: PlanningAppService) { 

    route.params.subscribe(p => { this.query.customerId = +p['id'] || 0})
  }

  ngOnInit() {
    this.populatePlanningAppSummary();
    this.refreshData();
    this.interval = setInterval(() => { 
        this.refreshData(); 
    }, 5000);
  }

  refreshData() {
    this.populatePlanningAppSummary();
  }

  private populatePlanningAppSummary() {
    console.warn("QUERY ID = " + this.query.customerId);
    this.PlanningAppService.getPlanningAppSummary(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onPageChange(page:any) {
    this.query.page = page; 
    this.populatePlanningAppSummary();
  }

  onFilterChange() {
    this.query.page = 1; 
    this.populatePlanningAppSummary();
  }
}





