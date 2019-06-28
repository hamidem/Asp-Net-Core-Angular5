import { Component, OnInit } from '@angular/core';
import { NavService } from '../shared/service/nav.service';
import { Nav } from '../shared/model/Feed';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css'],
    providers: [NavService]
})
export class NavMenuComponent implements OnInit {
    isExpanded = false;
    actuNav: Array<Nav>;
    scienceNav: Array<Nav>;
    cultureNav: Array<Nav>;
    sportNav: Array<Nav>;
    navService: NavService;
    urlIndex: number = 0;

    constructor(private service: NavService) {

    }

    ngOnInit() {
        this.urlIndex = 0;
        this.actuNav = this.service.actuNav;
        this.urlIndex = 0;
        this.scienceNav = this.service.scienceNav;
        this.urlIndex = 0;
        this.cultureNav = this.service.cultureNav;
        this.urlIndex = 0;
        this.sportNav = this.service.sportNav;
    }

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }
}