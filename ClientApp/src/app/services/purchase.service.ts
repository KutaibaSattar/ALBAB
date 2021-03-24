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

    super (environment.apiUrl + 'Purchases/purchinv', http);

   }

   private currentUserSource = new ReplaySubject<Purchase>(1); // buffer for only one user object

   currentUser$ = this.currentUserSource.asObservable(); // $ at end as convention that is Observable

// tslint:disable-next-line: typedef
getPurchase(){

 return this.getAll().pipe(
    map((purch: Purchase) => {
       if (purch){
        this.currentUserSource.next(purch);
        const purchase = purch[0];
        return purchase;
       }

       /*  (purchase).filter(item => item.idex === 0); */
    })

  );


}


}
