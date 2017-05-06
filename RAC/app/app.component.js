"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var app_services_1 = require("./app.services");
var app_models_1 = require("./app.models");
//import { Router } from '@angular/router';
//import { Observable } from 'rxjs/Observable';
//import { Subject } from 'rxjs/Subject';
//// Observable class extensions
//import 'rxjs/add/observable/of';
//// Observable operators
//import 'rxjs/add/operator/catch';
//import 'rxjs/add/operator/debounceTime';
//import 'rxjs/add/operator/distinctUntilChanged';
var AppComponent = (function () {
    function AppComponent(userServices) {
        this.userServices = userServices;
        this.user = new app_models_1.Models.User;
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.user.UserName = "harp99";
        this.user.pass = "leon";
        this.userServices.LogIn(this.user)
            .subscribe(function (x) {
            _this.user = x;
        });
    };
    AppComponent.prototype.GeneralError = function (data) {
        console.error = data;
    };
    return AppComponent;
}());
AppComponent = __decorate([
    core_1.Component({
        selector: 'RAC-app',
        templateUrl: "/app/app.component.html",
    }),
    __metadata("design:paramtypes", [app_services_1.UsersServices])
], AppComponent);
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map