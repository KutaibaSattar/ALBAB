import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Journal, JournalEntry } from 'app/models/journal';
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
    return this.getTableRecords('journallist').pipe(
      map((j: any) =>{
        return j.find(txn => txn.id === 1);

      })

    )
  }

  updateJournal(journal : Journal){
    this.createTableRecords(journal).subscribe(

      data => console.log(data)

    )

  }
}
