import { Injectable } from '@angular/core';
import { Nav } from '../model/Feed';

@Injectable()
export class NavService {

    public actuNav: Array<Nav>;
    public scienceNav: Array<Nav>;
    public cultureNav: Array<Nav>;
    public sportNav: Array<Nav>;

    constructor() {
        this.actuNav = [];
        this.scienceNav = [];
        this.cultureNav = [];
        this.sportNav = [];

        this.actuNav = [{ mediaName: 'Le Point' }, { mediaName: 'L\'Express' },
        { mediaName: 'La croix' }, { mediaName: 'Courrier International' }];

        this.scienceNav = [{ mediaName: 'L\'Express' }, { mediaName: 'Courrier International' }];

        this.cultureNav = [{ mediaName: 'Le Point' }, { mediaName: 'Courrier International' }];

        this.sportNav = [{ mediaName: 'Le Point' }, { mediaName: 'L\'Equipe' }, { mediaName: 'Sport.fr' }];
    }
}