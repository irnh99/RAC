import { Component } from '@angular/core';

export namespace Models {
    export class Area {
        IdArea: number;
        Name: string;
        Description: string;
        Status: boolean;
        HasAccess: Array<HasAccess>;;
    }

    export class HasAccess {
        IdHasAccess: number;
        IdUserType: number;
        IdArea: number;
    }

    export class User {
        IdUser: number;
        Name: string;
        pass: string;
        UserName: string;
        NoControl: number;
        UserType: UserType;
    }

    export class UserType {
        IdUSerType: number;
        Description: string;
        Users: Array<User>;
        HasAccess: Array<HasAccess>;
    }

    export class Access {
        IdAccess: number;
        user: User;
        Area: Area;
        Date: string;
    }
}