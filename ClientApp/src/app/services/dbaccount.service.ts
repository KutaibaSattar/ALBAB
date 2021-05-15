import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { dbAccounts, dbAccountsNewChild } from 'app/models/dbaccounts';
import { environment } from 'environments/environment';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class DbAccountService extends DataService {
  constructor(private httpClient: HttpClient) {
    super(environment.apiUrl + 'dbaccounts/', httpClient);
  }

  getDbAccounts() {
    return this.getTableRecords();
  }

  createDbAccount(dbaccount: dbAccountsNewChild) {
    if (dbaccount.id ==0) {
      return this.createTableRecords(dbaccount).pipe(
        map((res) => console.log(res))
      );
    }
  }
}
