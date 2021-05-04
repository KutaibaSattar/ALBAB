import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Journal } from 'app/models/journal';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class JournalService extends DataService {
  constructor(private httpClient: HttpClient) {
    super(environment.apiUrl + 'Journal/', httpClient);
  }

  getJournal() : Observable<Journal>  {
    return this.getAll('journallist').pipe(
      map((j: any) =>{
        return j.find(txn => txn.id === 1);


      })

    )
  }
}

