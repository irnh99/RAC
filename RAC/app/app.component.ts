import { Component, OnInit } from '@angular/core';
//import { UsersServices } from './app.services';
import { Models } from './app.models';
//import { Http, Headers, HttpModule } from '@angular/http';


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

export class AppComponent implements OnInit {

    user: Models.User = new Models.User;
    //constructor(private userServices: UsersServices) { }

    ngOnInit() {
        this.user.UserName = "harp99";
        this.user.pass = "leon";


        //this.userServices.LogIn(this.user)
        //    .map(x => this.user = x);

    }
    GeneralError(data: any) {
        console.error = data;
    }
}