import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class DbaccountService extends DataService {

  constructor(private httpClient: HttpClient) {

    super(environment.apiUrl + 'dbaccounts/', httpClient);

  }


  getAccounts(){

   return this.getAll()

  }
}
