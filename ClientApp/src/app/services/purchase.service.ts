import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PurchInv } from 'app/models/purchinv';
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
    map((purch: PurchInv) => {
       if (purch){
        const purchase = purch[0];
        return purchase;
       }

       /*  (purchase).filter(item => item.idex === 0); */
    })

  );

 }

 getPurchInv(id:number){

  return this.getById(id,'purchinv/');

 }

 UpdaePurchInv(id:number, purchase: any){

  console.log(purchase);

 //return this.http.put<Purchase>('environment.apiUrl',purchase);

 }




}
