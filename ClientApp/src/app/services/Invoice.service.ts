import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, Optional} from '@angular/core';
import { Invoice } from 'app/models/purchase';
import { APP_CONFIG } from 'app/_helper/InvoiceType-token';
import { environment } from 'environments/environment';
import { Observable, ReplaySubject } from 'rxjs';
import { map, retry } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable()
export class InvoiceService extends DataService {



  constructor( private httpClient: HttpClient,@Inject(APP_CONFIG) @Optional() public apiPoint?: string) {


    super(environment.apiUrl +  apiPoint, httpClient);
    console.log (apiPoint);
    //this.apiPoint = 'Hello';



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


  getInvLists(Apipoint : string) {

      return this.getTableRecords(Apipoint);
  }

  getLastList(){
    return this.getTableLastList('lastquote');

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

  protected getTableLastList  (resource)
  {
    return this.http.get(environment.apiUrl + this.apiPoint + resource);
  }
}
