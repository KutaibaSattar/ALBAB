import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { dbAccountStatement } from 'app/models/reports';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class ReportsService extends DataService {

  constructor(private httpClient: HttpClient) {
    super(environment.apiUrl + 'reports/', httpClient);
  }

  getAccountStatement()  {
    return this.getTableRecords('accountstatement').pipe(
      map((res: dbAccountStatement[]) => {
        return res;})
    );
  }



}
