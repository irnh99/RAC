import { Component, OnInit, OnDestroy } from '@angular/core';
import { Services } from './app.services';
import { Models } from './app.models';
import { Http, Headers, HttpModule } from '@angular/http';

//import { Router } from '@angular/router';

//import { Observable } from 'rxjs/Observable';
//import { Subject } from 'rxjs/Subject';

//// Observable class extensions
//import 'rxjs/add/observable/of';

//// Observable operators
//import 'rxjs/add/operator/catch';
//import 'rxjs/add/operator/debounceTime';
//import 'rxjs/add/operator/distinctUntilChanged';


@Component({
    selector: 'RAC-app',
    templateUrl: `/app/app.component.html`,
    //providers: [UsersServices]
})

export class AppComponent {

    page: string = "LogIn";
    isAdmin: boolean = false;


    //user vars
    user: Models.User = new Models.User;
    newUser: Models.User = new Models.User;

    //used list;
    areas: Models.Area[] = new Array<Models.Area>();
    accesses: Models.Access[] = new Array<Models.Access>();

    constructor(private services: Services) { }

    //ngOnInit() {
    //    this.user.UserName = "harp99";
    //    this.user.pass = "leon";


    //this.userServices.LogIn(this.user)
    //    .subscribe(x => {
    //        this.user = x;
    //    });
    //}

    LogIn(): void {

        if (this.user.Pass
            && this.user.Pass != ""
            && this.user.UserName
            && this.user.UserName != "") {

            this.services.LogIn(this.user)
                .subscribe(x => {
                    this.user = x;

                    this.isAdmin = (x.UserType.IdUserType == 0);

                    this.getAreas();
                });
        }
    }

    getAreas(): void {

        this.services.GetAreas(this.user.UserType.IdUserType)
            .subscribe(y => {
                this.areas = y;

                this.page = "Rooms";
            });
    }

    getAccesses(): void {

        this.services.GetAccesses()
            .subscribe(x => {
                this.accesses = x;

                this.page = "Accesses";
            });
    }


    LogOut(): void{
        this.page = "LogIn";
        this.isAdmin = false;
    }

    OpenClose(area: Models.Area): void {

        this.services.OpenClose(area)
            .subscribe(x => {
                this.getAreas();
            });
    }

    ChangePage(page: string): void {
        this.page = page;
    }

    GeneralError(data: any) {
        console.error = data;
    }
}