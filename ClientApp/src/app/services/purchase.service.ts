import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Purchase } from 'app/models/purchase';
import { environment } from 'environments/environment';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService extends DataService {



  constructor(http: HttpClient) {

    super (environment.apiUrl + 'Purchase/purchinv', http);

   }

// tslint:disable-next-line: typedef
getPurchase(){

 return this.getAll().pipe(
    map((response: Purchase) => {
        const purchase = response;
        if (purchase) {
            return purchase;

                 }
    })

  );


}


}
