import { StateStatusService } from './../../services/statestatus.service';
import { StateStatus } from './../../models/statestatus';
import { PlanningAppService } from '../../services/planningapp.service';
import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './planningapp-list.component.html'
})
export class PlanningAppListComponent implements OnInit {
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    stateStatus: {}
  };
  interval: any = {};

  stateStatuses: StateStatus[] = [];

  constructor(private PlanningAppService: PlanningAppService,
              private StateStatusService: StateStatusService) { }

  ngOnInit() {
    this.populatePlanningAppSummary();
    this.loadStatuses();
    // this.refreshData();
    // this.interval = setInterval(() => { 
    //     this.refreshData(); 
    // }, 5000);
  }

  // refreshData() {
  //   this.populatePlanningAppSummary();
  // }

  private loadStatuses() {
    this.StateStatusService.getStateStatuses(1)
      .subscribe(result => this.stateStatuses = result);
  }

  private populatePlanningAppSummary() {
    this.PlanningAppService.getPlanningAppSummary(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onPageChange(page:any) {
    this.query.page = page; 
    this.populatePlanningAppSummary();
  }

  onStateFilterChange() {
    console.warn("state = " + this.query.stateStatus);
    this.populatePlanningAppSummary();
  }
}

