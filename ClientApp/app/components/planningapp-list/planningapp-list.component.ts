import { PlanningAppService } from '../../services/planningapp.service';
import { Component, OnInit } from '@angular/core';
import { PlanningAppSummary } from '../../models/planningappsummary';

@Component({
  templateUrl: './planningapp-list.component.html'
})
export class PlanningAppListComponent implements OnInit {
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  interval: any = {};

  constructor(private PlanningAppService: PlanningAppService) { }

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

