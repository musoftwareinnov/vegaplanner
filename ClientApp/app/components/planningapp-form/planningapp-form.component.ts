import { Customer } from './../../models/customer';
import * as _ from 'underscore';
import { ProgressService } from '../../services/progress.service';
import { ChangePlanningAppState, PlanningApp } from '../../models/planningapp';
import { INITIAL_CONFIG } from '@angular/platform-server';
import { PlanningAppService } from '../../services/planningapp.service';
import { DrawingService } from '../../services/drawing.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewChild, ElementRef, NgZone } from '@angular/core';
import 'rxjs/add/Observable/forkJoin';
import { Observable } from 'rxjs/Observable';
import { ToastyService } from 'ng2-toasty';
import { Location } from '@angular/common';

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
    method: 1   //default to next state
  };

  planningApp: PlanningApp = {
    id: 0,
    // customerId: 0,
    customer: {
      id: 0, 
      firstName: "",
      lastName: "",
      address1: "",
      address2: "",
      postcode: "",
      emailAddress: "",
      telephoneHome: "",
      telephoneMobile:"",
      notes: "",
    },
    name: "",
    businessDate: "",
    planningStatus:  "",
    currentStateStatus: "",
    currentState:  "",
    expectedStateCompletionDate:  "",
    nextState:  "",
    completionDate:  "",
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

  submit() {
    //var result$ = (this.planningApp.id) ? this.planningAppService.update(this.vehicle) : this.vehicleService.create(this.vehicle); 

    this.savePlanningApp.method = 1;
    this.savePlanningApp.id = this.planningApp.id;
    console.warn( this.savePlanningApp);
    var result$ = this.planningAppService.nextState(this.savePlanningApp )

    if(this.planningApp.nextState == null)
        this.planningApp.nextState = this.COMPLETE;

    result$.subscribe(
        planningApp => {
          this.toastyService.success({
            title: 'Success', 
            msg: 'New State : ' + this.planningApp.nextState,
            theme: 'bootstrap',
            showClose: true,
            timeout: 5000
          })     
          this.router.navigate(['/planningapps/', this.planningApp.id])
          //this.router.navigate(['/planningapps/26']);
        });
    //location.reload();
  }

  onMakeChange() {
    // this.populateModels();
    // delete this.vehicle.modelId;
  }

  private populateModels() {
    // var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    // this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId: any, $event: any) {
    // if($event.target.checked)
    //   this.vehicle.features.push(featureId);
    // else {
    //   var index = this.vehicle.features.indexOf(featureId);
    //   this.vehicle.features.splice(index, 1);
    // }
  
  }
  
  delete() {
    // if (confirm("Are you sure?")) {
    //   this.planningAppService.delete(this.vehicle.id)
    //     .subscribe(x => { this.router.navigate(['/home'])});
    // }
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

