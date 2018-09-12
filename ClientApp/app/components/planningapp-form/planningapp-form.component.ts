import { Customer } from './../../models/customer';
import * as _ from 'underscore';
import { ProgressService } from '../../services/progress.service';
import { ChangePlanningAppState, PlanningApp, SavePlanningNotes } from '../../models/planningapp';
import { INITIAL_CONFIG } from '@angular/platform-server';
import { PlanningAppService } from '../../services/planningapp.service';
import { DrawingService } from '../../services/drawing.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewChild, ElementRef, NgZone } from '@angular/core';
//import 'rxjs/add/Observable/forkJoin';
import { Observable } from 'rxjs';
import { ToastyService } from 'ng2-toasty';
import { Location } from '@angular/common';
import { StateAction } from '../../constants';

@Component({
  selector: 'app-planingapp-form',
  templateUrl: './planningapp-form.component.html',
  styleUrls: ['./planningapp-form.component.css']
})

export class PlanningAppFormComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef | undefined;

  private readonly COMPLETE = 'Complete';
  vehicleId: number = 0; 
  drawings: any[] = [];
  progress: any;

  savePlanningApp: ChangePlanningAppState = {
    id: 0,
    method: StateAction.Prev  //default to next state

  };

  plannningNotes: SavePlanningNotes = {
    id:0,
    notes: ""
  };

  planningApp: PlanningApp = {
    id: 0,
    // customerId: 0,
    customer: {
      id: 0, 
      firstName: "",
      lastName: "",
      addressLine1: "",
      addressLine2: "",
      postcode: "",
      emailAddress: "",
      telephoneHome: "",
      telephoneMobile:"",
      telephoneWork:"",
      notes: "",
    },
    name: "",
    businessDate: "",
    planningStatus:  "",
    currentStateStatus: "",
    currentState:  "",
    expectedStateCompletionDate:  "",
    nextState:  "",
    councilPlanningAppId: "",
    completionDate:  "",
    generator: "",
    notes: "",
    planningAppStates: [],
    method: 1
  };

  interval: any = {};

  constructor(
    private route: ActivatedRoute,
    private zone: NgZone,
    private router: Router,
    private toastyService: ToastyService,
    private progressService: ProgressService,
    private drawingServices: DrawingService,
    private planningAppService: PlanningAppService,
    private location: Location) { 

    route.params.subscribe(p => { this.planningApp.id = +p['id'] || 0}); }
    
  ngOnInit() {
    this.drawingServices.getDrawings(this.planningApp.id)
      .subscribe(drawings => this.drawings = drawings);

    this.planningAppService.getPlanningApp(this.planningApp.id)
    .subscribe(
      v => this.planningApp = v,
      err => {
        if (err.status == 404) {
          this.router.navigate(['/planningapps']);
          return; 
        }
    });

    // this.refreshData();
    // this.interval = setInterval(() => { 
    //     this.refreshData(); 
    // }, 5000);
  }

  refreshData() {
    this.populatePlanningAppSummary();
  }

  private populatePlanningAppSummary() {
    this.planningAppService.getPlanningApp(this.planningApp.id)
    .subscribe(
      v => this.planningApp = v,
      err => {
        if (err.status == 404) {
          this.router.navigate(['/planningapps']);
          return; 
        }
    });
  }

  //Not used - but useful - just refreshData data intead of whole page as better user experience
  load() {
    location.reload()
    }

  nextState() {
    this.savePlanningApp.id = this.planningApp.id;

    //Check conditions have been set before saving
    
    var result = this.planningAppService.nextState(this.savePlanningApp )

    if(this.planningApp.nextState == null)
        this.planningApp.nextState = this.COMPLETE;

    result.subscribe(

        planningApp => {
          this.toastyService.success({
            title: 'Success', 
            msg: 'New State : ' + this.planningApp.nextState,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })
          { this.refreshData() }   
          this.router.navigate(['/planningapps/', this.planningApp.id])
        },

        error => {
            console.warn("Error!!!!");
            this.toastyService.error({
            title: 'Please enter mandatory fields in state requirements tab', 
            msg: "",
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })
        },  
        );
  }

  saveNotes() {
    this.plannningNotes.id = this.planningApp.id;
    this.plannningNotes.notes = this.planningApp.notes;
    console.warn("notes:" + this.plannningNotes.notes);
    var result$ = this.planningAppService.saveNotes(this.plannningNotes )
    result$.subscribe(
      planningApp => {
        this.toastyService.success({
          title: 'Success', 
          msg: 'Notes updated ',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
  }

  saveDevelopmentDetails() {
    this.plannningNotes.id = this.planningApp.id;
    this.plannningNotes.notes = this.planningApp.notes;
    console.warn("notes:" + this.plannningNotes.notes);
    var result$ = this.planningAppService.saveDevelopmentDetails(this.planningApp )
    result$.subscribe(
      planningApp => {
        this.toastyService.success({
          title: 'Success', 
          msg: 'Notes updated ',
          theme: 'bootstrap',
          showClose: true,
          timeout: 5000
        })
      });
  }
  
  archive() {
    if (confirm("Archiving Not Installed, extra module")) {
        
    }
  }

  terminate() {
    if (confirm("Confirm : Terminate Planning App")) {
      this.savePlanningApp.id = this.planningApp.id;
      var result$ = this.planningAppService.terminate(this.savePlanningApp )
  
      if(this.planningApp.nextState == null)
          this.planningApp.nextState = this.COMPLETE;
  
      result$.subscribe(
          planningApp => {
            this.toastyService.success({
              title: 'Success', 
              msg: 'Planning App : ' + this.planningApp.id + " Terminated",
              theme: 'bootstrap',
              showClose: true,
              timeout: 5000
            })     
            this.router.navigate(['/planningapps/', this.planningApp.id])
          });
    }
  }

  uploadDrawings() {

    if(this.fileInput) {
      var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

      if(nativeElement.files != null)
      {
        console.warn(this.fileInput)
        this.progressService.startTracking()
          .subscribe(progress => {
            console.log(progress);
            this.zone.run(() => {
              this.progress = progress;
            });
          },
          undefined,
          () => { this.progress = null });

        this.drawingServices.upload(this.planningApp.id, nativeElement.files[0])
          .subscribe(drawings => {
              this.drawings.push(drawings)
          });
      }
    }
  }
}

