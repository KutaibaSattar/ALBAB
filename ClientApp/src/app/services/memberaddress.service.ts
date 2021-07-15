import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MemberAddress } from 'app/models/memberaddress';
import { environment } from 'environments/environment';
import { BehaviorSubject, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class MemberAddressService extends DataService {

  constructor( private httpClient: HttpClient) {


    super(environment.apiUrl+ 'address', httpClient );

  }

  public memberAddressSource$ = new ReplaySubject<MemberAddress[]>(null);

  getAddressList()  {
    return this.getTableRecords().pipe(
       map((res: MemberAddress[]) => {
        //this.products = res;
       this.memberAddressSource$.next(res)
       return res;
       })
     );
      }
}
