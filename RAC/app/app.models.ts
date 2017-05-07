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
        Pass: string;
        UserName: string;
        NoControl: number;
        UserType: UserType;
    }

    export class UserType {
        IdUserType: number;
        Description: string;
        Users: Array<User>;
        HasAccess: Array<HasAccess>;
    }

    export class Access {
        IdAccess: number;
        User: User;
        Area: Area;
        Date: string;
    }
}