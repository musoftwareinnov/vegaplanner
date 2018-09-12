import { PlanningAppState } from './../../models/PlanningAppState';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastyService } from 'ng2-toasty';
import { PlanningAppStateService } from '../../services/planninappstate.service';
import * as moment from 'moment';

@Component({
  selector: 'app-planningappstate-form',
  templateUrl: './planningappstate-form.component.html',
  styleUrls: ['./planningappstate-form.component.css'],
})
export class PlanningAppStateFormComponent implements OnInit {
  planningAppState: PlanningAppState = {
      id: 0, 
      stateName: "",
      dueByDate: "",
      dateCompleted: "",
      stateStatus: "",
      currentState: false,
      minDueByDate: "",
      dueByDateEditable: false,
      stateRules: [],
      isCustomDuration: false,
      notes: "",
    };
  
  updatedDueByDate: Date = new Date();
  calMinDate: Date = new Date();
  calDueByDate: Date = new Date();
  updateNotes: string = ""
  bsValue = new Date();
  bsRangeValue: Date[];
  maxDate = new Date();
  
  events: any[];
  tomorrow: Date;
   afterTomorrow: Date;
   ignoreWeekends: number[];
   dateDisabled: { date: Date; mode: string }[];
   formats: string[] = [
     'DD-MM-YYYY',
     'YYYY/MM/DD',
     'DD.MM.YYYY',
     'shortDate'
   ];
   format: string = this.formats[0];
   dateOptions: any = {
     formatYear: 'YY',
     startingDay: 1
   };
   private opened: boolean = false;

   private conditionList: string[] = [ "planningAppId", "BRR Dates" ]
  
   private conList: { conditionName:any, conditionValue: any } [];

  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastyService: ToastyService,
    private planningAppStateService: PlanningAppStateService) { 

    route.params.subscribe(p => { this.planningAppState.id = +p['id'] || 0})

    //Calender Specific Settings
    this.maxDate.setDate(this.maxDate.getDate() + 7);
    this.bsRangeValue = [this.bsValue, this.maxDate];
    (this.tomorrow = new Date()).setDate(this.tomorrow.getDate() + 1);
    (this.afterTomorrow = new Date()).setDate(this.tomorrow.getDate() + 2);
    this.dateDisabled = [];
    this.ignoreWeekends = [0,6];
    this.events = [
      { date: this.tomorrow, status: 'full' },
      { date: this.afterTomorrow, status: 'partially' }
    ];
    // End of Calendar settings


    this.conList = [ {conditionName: "planningAppId", conditionValue: ""}, {conditionName: "Build Regs Date1", conditionValue: ""} ]

    }

  ngOnInit() {
    
    if (this.planningAppState.id)
        this.planningAppStateService.getPlanningAppState(this.planningAppState.id)
        .subscribe(
          planningAppState => { 
              this.planningAppState = planningAppState;
              this.calMinDate = this.getMinDate();  //Format the date for the js calendar
              this.calDueByDate = this.getDueByDate();
              this.updatedDueByDate = this.getDueByDate();
             },
          err => {
            if (err.status == 404) {
              this.router.navigate(['/planningappstate/', this.planningAppState.id]);
              return; 
            }

        });
  }

  submit() {
    this.planningAppState.dueByDate  = moment(this.updatedDueByDate).format('DD-MM-YYYY');
    var result$ = this.planningAppStateService.updatePlanningAppState(this.planningAppState); 

    console.warn(this.planningAppState.stateRules);

    result$.subscribe(
      planningAppState => {
      this.toastyService.success({
        title: 'Success', 
        msg: 'Planning App State was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      })
  
      //this.router.navigate(['/planningapps']);
    });
  }


  saveRules() {
    this.planningAppState.dueByDate  = moment(this.updatedDueByDate).format('DD-MM-YYYY');
    var result$ = this.planningAppStateService.updatePlanningAppState(this.planningAppState); 

    console.warn(this.planningAppState.stateRules);

    result$.subscribe(
      planningAppState => {
      this.toastyService.success({
        title: 'Success', 
        msg: 'Planning App State was sucessfully saved.',
        theme: 'bootstrap',
        showClose: true,
        timeout: 5000
      })
  
      //this.router.navigate(['/planningapps']);
    });
  }

  getDate(): number {
    return (this.updatedDueByDate && this.updatedDueByDate.getTime()) || new Date().getTime();
  }

  getDueByDate(): Date {
    var dateParts = this.planningAppState.dueByDate.split("-");
    return new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
 }

  getMinDate(): Date {
    var dateParts = this.planningAppState.minDueByDate.split("-");
    return new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
 }
 today(): void {
  this.updatedDueByDate = new Date();
}

open(): void {
  this.opened = !this.opened;
}

clear(): void {
  this.updatedDueByDate = new Date();
  this.dateDisabled;
}

getDayClass(date: any, mode: string): string {
  if (mode === 'day') {
    let dayToCheck = new Date(date).setHours(0, 0, 0, 0);

    for (let event of this.events) {
      let currentDay = new Date(event.date).setHours(0, 0, 0, 0);

      if (dayToCheck === currentDay) {
        return event.status;
      }
    }
  }

  return '';
}

}

