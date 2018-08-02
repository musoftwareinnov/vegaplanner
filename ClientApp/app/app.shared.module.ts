import { BrowserXhrWithProgress, ProgressService } from './services/progress.service';
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, BrowserXhr } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
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
import { PlanningAppFormComponent } from './components/planningapp-form/planningapp-form.component';
import { StateInitialiserListComponent } from './components/stateinitialiser-list/stateinitialiser-list.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { CustomerPlanningAppListComponent } from './components/customerplanningapp-list/customerplanningapp-list.component';


import { VehicleService } from './services/vehicle.service';
import { PlanningAppService } from './services/planningapp.service';
import { PhotoService } from './services/photo.service';
import { CustomerService } from './services/customer.service';
import { AppErrorHandler } from './app.error.handler';
  
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
        StateInitialiserListComponent,
        CustomerListComponent,
        CustomerFormComponent,
        CustomerPlanningAppListComponent,
        PaginationComponent,
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: 'planningapps/new', component: PlanningAppNewComponent },
            { path: 'planningapps', component: PlanningAppListComponent },
            { path: 'planningapps/:id', component: PlanningAppFormComponent },
            { path: 'stateinitialisers', component: StateInitialiserListComponent },
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
        PhotoService,
        ProgressService,
        CustomerService
    ]
})
export class AppModuleShared {
}
