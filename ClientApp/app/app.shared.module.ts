import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
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

import { VehicleService } from './services/vehicle.service';
import { PhotoService } from './services/photo.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        HomeComponent,
        VehicleFormComponent,
        ViewVehicleComponent,
        VehicleListComponent,
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
            { path: '**', redirectTo: 'home' }
        ])
    ],

    providers : [
        VehicleService,
        PhotoService
    ]
})
export class AppModuleShared {
}
