import { Component, OnInit, OnDestroy } from '@angular/core';
import { StatisticsService } from '../../services/statistics.service';
import { Statistics } from '../../models/statistics';
import { UserService } from '../../shared/services/user.service';
import { Subscription} from 'rxjs/Subscription';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})



export class NavMenuComponent implements OnInit, OnDestroy {

    loginStatus: boolean = false;
    userName: string = "";
    subscription:Subscription = new Subscription;
    userName$:Subscription = new Subscription;

    planningStatistics: Statistics = {
        inProgress:0,
        onTime:0,
        due:0,
        overdue:0,
        completed:0,
        terminated:0,
        archived:0,
        overan:0,
        all:0
      };

    interval: any = {};

    constructor(private statisticsService: StatisticsService, private userService:UserService)  { }

    ngOnInit() {

        this.populateStatistics();

        this.interval = setInterval(() => { 
            this.populateStatistics(); 
        }, 10000);

        //User logging
        this.subscription = this.userService.authNavStatus$.subscribe(status => this.loginStatus = status);
        this.userName$ = this.userService.authNavUser$.subscribe(userName => this.userName = userName);
      }

      private populateStatistics() {
          this.statisticsService.getPlanningStatistics()
          .subscribe(
            v => this.planningStatistics = v
          );
      }

    logout() {
        this.userService.logout();       
     }

     ngOnDestroy() {
      // prevent memory leak when component is destroyed
      this.subscription.unsubscribe();
    }
}
