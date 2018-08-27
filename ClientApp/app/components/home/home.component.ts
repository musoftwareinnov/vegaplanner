import { Component, NgModule } from '@angular/core';
import { ButtonsModule, AlertModule } from 'ngx-bootstrap';
import { DatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
// import {
//     AlertModule, 
// } from 'ngx-bootstrap';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

@NgModule({
    imports: [
        BsDatepickerModule.forRoot()
    ]})


export class HomeComponent {

    bsValue = new Date();
    bsRangeValue: Date[];
    maxDate = new Date();


    numDate: number = 0;
    model: any;
    dt: Date = new Date();
    minDate: Date = new Date("15/08/2018");
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
   
    constructor() {
        this.maxDate.setDate(this.maxDate.getDate() + 7);
        this.bsRangeValue = [this.bsValue, this.maxDate];
      (this.tomorrow = new Date()).setDate(this.tomorrow.getDate() + 1);
      (this.afterTomorrow = new Date()).setDate(this.tomorrow.getDate() + 2);
      this.minDate = this.getMinDate();
      //(this.minDate = new Date().setDate()
      this.dateDisabled = [];
      this.events = [
        { date: this.tomorrow, status: 'full' },
        { date: this.afterTomorrow, status: 'partially' }
      ];

      this.ignoreWeekends = [0,6];
    }
   
    getMinDate(): Date {

       var mDate = "15-08-2018";
       var dateParts = mDate.split("-");
       var minDate = new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
       return minDate;
    }

    getDate(): number {
    //   return (this.dt && this.dt.getTime()) || new Date().getTime();

         this.numDate = (this.dt && this.dt.getTime()) || new Date().getTime();
         
         return this.numDate;
    }
   
    today(): void {
      this.dt = new Date();
    }
   
    d20090824(): void {
      this.dt = new Date(2009, 7, 24);
    }
   
    disableTomorrow(): void {
      this.dateDisabled = [{ date: this.tomorrow, mode: 'day' }];
    }
   
    // todo: implement custom class cases
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
   
    disabled(date: Date, mode: string): boolean {
      return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }
   
    open(): void {
      this.opened = !this.opened;
    }
   
    clear(): void {
      this.dt = new Date();
      this.dateDisabled;
    }
   
    toggleMin(): void {
      this.dt = new Date(this.minDate.valueOf());
    }
  }
