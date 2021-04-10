import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Purchase } from 'app/models/purchase';
import { environment } from 'environments/environment';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService extends DataService {



  constructor(http: HttpClient) {

    super (environment.apiUrl + 'Purchases/', http);

   }



// tslint:disable-next-line: typedef
getPurchases(){

 return this.getAll('purchases').pipe(
    map((purch: Purchase) => {
       if (purch){
        const purchase = purch[0];
        return purchase;
       }

       /*  (purchase).filter(item => item.idex === 0); */
    })

  );

 }

 getPurchInv(InvNo:string){

  return this.getById(InvNo,'purchinv/');

 }

 UpdaePurchInv( purchase: Purchase){


 //return this.http.put<Purchase>(environment.apiUrl + 'Purchases',purchase);
 return this.http.put<Purchase>('https://localhost:5001/api/Purchases',purchase);

 }




}
