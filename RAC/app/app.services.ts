import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, HttpModule, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Models } from './app.models'

@Injectable()
export class Services {
    /* production
    private generalUrl = 'http://ec2-54-244-184-79.us-west-2.compute.amazonaws.com/';
    /*local test*/
    private generalUrl = 'http://localhost:51508/';
    /**/
    private loginUrl = 'Users/Login'
    private getRoomsUrl = 'Areas/'
    private openCloseUrl = 'Areas/OpenClose'
    private getAccessesUrl = 'Accesses/'
    private CreateAccessUrl = 'Accesses/Create'


    constructor(private http: Http) { }

    ///User
    //LogIn
    LogIn(user: Models.User): Observable<Models.User> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.generalUrl + this.loginUrl, { user: user }, options)
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }
    ///Rooms
    //GetRooms
    GetAreas(UserTypeId: number): Observable<Models.Area[]> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('idUserType', UserTypeId.toString());

        let requestOptions = new RequestOptions();
        requestOptions.search = params;

        let headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.get(this.generalUrl + this.getRoomsUrl, requestOptions)
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }
    //open close area
    OpenClose(area: Models.Area): Observable<string> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.generalUrl + this.openCloseUrl, { area: area }, options)
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }
    ///Accesses
    //get accesses
    GetAccesses(): Observable<Models.Access[]> {
        let headers = new Headers({ 'Content-Type': 'application/json' });

        return this.http.get(this.generalUrl + this.getAccessesUrl)
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }
    //create access register
    CreateAccess(access: Models.Access): Observable<string> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.generalUrl + this.CreateAccessUrl, { accessVm: access }, options)
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    }
    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
