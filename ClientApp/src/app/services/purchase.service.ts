import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IInvoice } from 'app/models/purchase';
import { environment } from 'environments/environment';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class PurchaseService extends DataService {

  constructor(private httpClient: HttpClient) {

    super(environment.apiUrl + 'Invoices/', httpClient);

  }


  getPurchases() {
    return this.getTableRecords('purchlist').pipe(
      map((purch: IInvoice) => {
        if (purch) {
          const purchase = purch[0];
          return purchase;
        }

      })
    );
  }


  getPurchNos() {
    return this.httpClient.get<Observable<any>>(
      environment.apiUrl + 'invoices/purchnos'
    );
  }

  getPurchInv(InvNo: string) {
    return this.getTableRecordId(InvNo, 'purchinv/');
  }

  UpdaePurchInv(purchase: IInvoice) {
    if (purchase.id) return this.http.put<IInvoice>('https://localhost:5001/api/invoices',purchase);

    return this.http.post<IInvoice>('https://localhost:5001/api/invoices',purchase);
    //return this.http.put<Purchase>(environment.apiUrl + 'Purchases',purchase);
  }

  deletePurchase(id: number) {

   return this.deleteTableRecord(id);

  }
}
