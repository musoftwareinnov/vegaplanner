import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '../../services/statistics.service';
import { Statistics } from '../../models/statistics';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
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

    constructor(private statisticsService: StatisticsService)  { }

    ngOnInit() {
        this.populateStatistics();

        this.interval = setInterval(() => { 
            this.populateStatistics(); 
        }, 10000);
      }

      private populateStatistics() {
        this.statisticsService.getPlanningStatistics()
        .subscribe(
          v => this.planningStatistics = v
        );
      }

}
