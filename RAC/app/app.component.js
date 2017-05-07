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
    function AppComponent(services) {
        this.services = services;
        this.page = "LogIn";
        this.isAdmin = false;
        //user vars
        this.user = new app_models_1.Models.User;
        this.newUser = new app_models_1.Models.User;
        //used list;
        this.areas = new Array();
        this.accesses = new Array();
    }
    //ngOnInit() {
    //    this.user.UserName = "harp99";
    //    this.user.pass = "leon";
    //this.userServices.LogIn(this.user)
    //    .subscribe(x => {
    //        this.user = x;
    //    });
    //}
    AppComponent.prototype.LogIn = function () {
        var _this = this;
        if (this.user.Pass
            && this.user.Pass != ""
            && this.user.UserName
            && this.user.UserName != "") {
            this.services.LogIn(this.user)
                .subscribe(function (x) {
                _this.user = x;
                _this.isAdmin = (x.UserType.IdUserType == 0);
                _this.getAreas();
            });
        }
    };
    AppComponent.prototype.getAreas = function () {
        var _this = this;
        this.services.GetAreas(this.user.UserType.IdUserType)
            .subscribe(function (y) {
            _this.areas = y;
            _this.page = "Rooms";
        });
    };
    AppComponent.prototype.getAccesses = function () {
        var _this = this;
        this.services.GetAccesses()
            .subscribe(function (x) {
            _this.accesses = x;
            _this.page = "Accesses";
        });
    };
    AppComponent.prototype.LogOut = function () {
        this.user = new app_models_1.Models.User();
        this.page = "LogIn";
        this.isAdmin = false;
    };
    AppComponent.prototype.OpenClose = function (area) {
        var _this = this;
        this.services.OpenClose(area)
            .subscribe(function (x) {
            if (!area.Status) {
                _this.CreateAccess(area);
            }
            _this.getAreas();
        });
    };
    AppComponent.prototype.CreateAccess = function (area) {
        var access = new app_models_1.Models.Access();
        access.Area = area;
        access.User = this.user;
        this.services.CreateAccess(access)
            .subscribe(function (x) { });
    };
    AppComponent.prototype.ChangePage = function (page) {
        this.page = page;
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
    __metadata("design:paramtypes", [app_services_1.Services])
], AppComponent);
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map