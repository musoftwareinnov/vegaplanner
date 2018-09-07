import { BrowserXhrWithProgress, ProgressService } from './services/progress.service';
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserXhr } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { AlertModule } from 'ngx-bootstrap/alert';
import { DatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { PaginationComponent } from './components/shared/pagination.component';
import { PlanningAppNewComponent } from './components/planningapp-new/planningapp-new.component';
import { PlanningAppListComponent } from './components/planningapp-list/planningapp-list.component';
import { PlanningAppListCompletedComponent } from './components/planningappcompleted-list/planningappcompleted-list.component';
import { PlanningAppFormComponent } from './components/planningapp-form/planningapp-form.component';
import { PlanningAppStateFormComponent } from './components/planningappstate-form/planningappstate-form.component';
import { StateInitialiserListComponent } from './components/stateinitialiser-list/stateinitialiser-list.component';
import { StateInitialiserFormComponent } from './components/stateinitialiser-form/stateinitialiser-form.component';
import { StateInitialiserStateListComponent } from './components/stateinitialiserstate-list/stateinitialiserstate-list.component';
import { StateInitialiserStateFormComponent } from './components/stateinitialiserstate-form/stateinitialiserstate-form.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { CustomerPlanningAppListComponent } from './components/customerplanningapp-list/customerplanningapp-list.component';

import { VehicleService } from './services/vehicle.service';
import { PlanningAppService } from './services/planningapp.service';
import { PhotoService } from './services/photo.service';
import { StateInitialiserService } from './services/stateinitialiser.service';
import { StateInitialiserStateService } from './services/stateinitialiserstate.service';
import { CustomerService } from './services/customer.service';
import { AppErrorHandler } from './app.error.handler';
import { DrawingService } from './services/drawing.service';
import { StateStatusService } from './services/statestatus.service';
import { PlanningAppStateService } from './services/planninappstate.service';
import { StatisticsService } from './services/statistics.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        HomeComponent,
        VehicleFormComponent,
        ViewVehicleComponent,
        VehicleListComponent,
        PlanningAppNewComponent,
        PlanningAppListComponent,
        PlanningAppFormComponent,
        PlanningAppStateFormComponent,
        PlanningAppListCompletedComponent,
        StateInitialiserListComponent,
        StateInitialiserFormComponent,
        StateInitialiserStateListComponent,
        StateInitialiserStateFormComponent,
        CustomerListComponent,
        CustomerFormComponent,
        CustomerPlanningAppListComponent,
        PaginationComponent,
    ],
    exports: [
        ButtonsModule,
        AlertModule,
        DatepickerModule,
        BsDatepickerModule
      ],
    imports: [
        CommonModule,    
        HttpClientModule,
        FormsModule,
        ButtonsModule.forRoot(),
        AlertModule.forRoot(),
        DatepickerModule.forRoot(),
        BsDatepickerModule.forRoot(),
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'planningapps/completed', component: PlanningAppListCompletedComponent },
            { path: 'planningapps/new', component: PlanningAppNewComponent },
            { path: 'planningapps', component: PlanningAppListComponent },
            { path: 'planningapps/all', component: PlanningAppListComponent },
            { path: 'planningapps/:id', component: PlanningAppFormComponent },
            { path: 'planningappstate/:id', component: PlanningAppStateFormComponent },
            { path: 'stateinitialisers', component: StateInitialiserListComponent },
            { path: 'stateinitialisers/new', component: StateInitialiserFormComponent },
            { path: 'stateinitialisers/:id', component: StateInitialiserStateListComponent },
            { path: 'stateinitialiserstate/:id', component: StateInitialiserStateFormComponent },
            { path: 'customers', component: CustomerListComponent },
            { path: 'customers/new', component: CustomerFormComponent },
            { path: 'customers/edit/:id', component: CustomerFormComponent },
            { path: 'customers/planningapps/:id', component: CustomerPlanningAppListComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],

    providers : [
        { provide: ErrorHandler, useClass: AppErrorHandler},
        { provide: BrowserXhr, useClass: BrowserXhrWithProgress},
        VehicleService,
        PlanningAppService,
        PlanningAppStateService,
        PhotoService,
        DrawingService,
        ProgressService,
        CustomerService,
        StateStatusService,
        StateInitialiserService,
        StateInitialiserStateService,
        StatisticsService
    ]
})
export class AppModuleShared {
}
