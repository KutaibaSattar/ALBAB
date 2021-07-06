import { Injectable, InjectionToken } from '@angular/core';
import { AuthService } from 'app/services/auth.service';
//import { OpaqueToken } from "@angular/core";


export interface IAppConfig {
    apiEndpoint: string;
}


export let APP_CONFIG = new InjectionToken<string>('');

export const AppConfig: IAppConfig = {
    apiEndpoint: ''
};


//export const APICONTROLLER =  new InjectionToken<string>('');
