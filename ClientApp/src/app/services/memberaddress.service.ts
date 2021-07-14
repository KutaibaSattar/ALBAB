import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MemberAddress } from 'app/models/memberaddress';
import { environment } from 'environments/environment';
import { map } from 'rxjs/operators';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class MemberAddressService extends DataService {

  constructor( private httpClient: HttpClient) {


    super(environment.apiUrl+ 'address', httpClient );

  }

  getAddressList()  {
    return this.getTableRecords().pipe(
       map((res: MemberAddress[]) => {
        //this.products = res;
       console.log(res)
       return res;
       })
     );
      }
}
