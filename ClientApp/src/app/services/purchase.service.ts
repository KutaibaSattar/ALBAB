import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPurchase } from 'app/models/purchase';
import { environment } from 'environments/environment';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class PurchaseService extends DataService {

  constructor(private httpClient: HttpClient) {

    super(environment.apiUrl + 'Purchases/', httpClient);

  }


  getPurchases() {
    return this.getTableRecords('purchlist').pipe(
      map((purch: IPurchase) => {
        if (purch) {
          const purchase = purch[0];
          return purchase;
        }

      })
    );
  }


  getPurchNos() {
    return this.httpClient.get<Observable<any>>(
      environment.apiUrl + 'purchases/purchnos'
    );
  }

  getPurchInv(InvNo: string) {
    return this.getTableRecordId(InvNo, 'purchinv/');
  }

  UpdaePurchInv(purchase: IPurchase) {
    if (purchase.id)
      return this.http.put<IPurchase>(
        'https://localhost:5001/api/Purchases',
        purchase
      );
    return this.http.post<IPurchase>(
      'https://localhost:5001/api/Purchases',
      purchase
    );
    //return this.http.put<Purchase>(environment.apiUrl + 'Purchases',purchase);
  }

  deletePurchase(id: number) {

   return this.deleteTableRecord(id);

  }
}
