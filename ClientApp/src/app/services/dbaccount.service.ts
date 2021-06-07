import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { dbAccounts, dbAccountsNewChild } from 'app/models/dbaccounts';
import { environment } from 'environments/environment';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class DbAccountService extends DataService {
  constructor(private httpClient: HttpClient) {
    super(environment.apiUrl + 'dbaccounts/', httpClient);
  }


  public CurrentAccountService = new BehaviorSubject<dbAccounts[]>(null);
  public accountSource$ = this.CurrentAccountService.asObservable();

  accounts : dbAccounts[];

  getDbAccounts() {
    return this.getTableRecords().pipe(

      map( (res:dbAccounts[]) => {

            this.accounts = res;
            this.CurrentAccountService.next(this.accounts);
            return this.accounts;
      }
      )

    );
  }

  createDbAccount(dbaccount: dbAccountsNewChild) {
    if (dbaccount.id ==0) {
      return this.createTableRecords(dbaccount).pipe(
        map((res) => console.log(res))
      );
    }
  }
}
