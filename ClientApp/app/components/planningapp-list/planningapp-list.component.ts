import { UserService } from './../../shared/services/user.service';
import { StateStatusService } from './../../services/statestatus.service';
import { StateStatus } from './../../models/statestatus';
import { PlanningAppService } from '../../services/planningapp.service';
import { Component, OnInit } from '@angular/core';
import { ToastyService } from 'ng2-toasty';
import { AuthGuard } from '../../auth.guard';

@Component({
  templateUrl: './planningapp-list.component.html'
})
export class PlanningAppListComponent implements OnInit {
  private readonly PAGE_SIZE = 10; 
  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE,
    stateStatus: {},
    planningAppType: ""
  };
  interval: any = {};

  stateStatuses: StateStatus[] = [];

  constructor(private PlanningAppService: PlanningAppService,
              private StateStatusService: StateStatusService,
              private toastyService: ToastyService,
              private authGuard:AuthGuard,
              private userService: UserService) {

               authGuard.canActivate();
               }

  ngOnInit() {
      this.toastyService.wait({
        title: 'Initialising', 
        msg: 'Loading Applications.....',
        theme: 'bootstrap',
        showClose: false,
        timeout: 2000
      });
      this.loadStatuses();
      this.refreshData();
      this.interval = setInterval(() => { 
          this.refreshData(); 
      }, 5000);
  
  }

  refreshData() {
      console.info("PlanningAppListComponent: Heartbeat");
      this.populatePlanningAppSummary();
  }

  private loadStatuses() {
    this.StateStatusService.getStateStatuses("InProgress")
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
    console.info("PlanningAppListComponent: Type Change = " + this.query.planningAppType);
    this.populatePlanningAppSummary();
  }

  ngOnDestroy() {   //Stop the planning service being called when user logs off
    console.warn("DESTROY CALLED")
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}

