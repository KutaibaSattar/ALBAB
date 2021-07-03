import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Invoice } from 'app/models/purchase';
import { environment } from 'environments/environment';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService extends DataService {

  constructor(private httpClient: HttpClient) {

    super(environment.apiUrl + 'purchase/', httpClient);

  }



  getPurchases() {
    return this.getTableRecords('purchlist').pipe(
      map((purch: Invoice) => {
        if (purch) {
          const purchase = purch[0];
          return purchase;
        }

      })
    );
  }


  getInvLists() {
    return this.getTableRecords('purchListNo');
  }

  getPurchInv(InvNo: string) {
    return this.getTableRecordId(InvNo, 'invoice');
  }

  UpdaePurchInv(purchase: Invoice) {
    if (purchase.id)
    return this.UpdateTable( purchase);
    //return this.http.put<IInvoice>('https://localhost:5001/api/invoices',purchase);

    return this.createTableRecords(purchase);
    //return this.http.put<Purchase>(environment.apiUrl + 'Purchases',purchase);
  }

  deletePurchase(id: number) {

   return this.deleteTableRecord(id);

  }
}