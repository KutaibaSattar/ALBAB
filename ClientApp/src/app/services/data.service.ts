import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {



  constructor(private baseUrl: string, protected http: HttpClient) {



   }




  // tslint:disable-next-line: typedef
 protected getTableRecords(extraLocation: string ='') {
    return this.http.get(this.baseUrl + extraLocation);

  }

  // tslint:disable-next-line: typedef
  protected createTableRecords(resource,extraLocation: string =''){
    return this.http.post(this.baseUrl+ extraLocation , resource );

  }

  // tslint:disable-next-line: typedef
  protected deleteTableRecord(id){
      return this.http.delete(this.baseUrl + id);
  }

  protected getTableRecordId(id,extraLocation: string ='') : Observable<any>{

   return this.http.get(this.baseUrl + extraLocation + '/' + id);

  }

  protected UpdateTable(resource,extraLocation: string ='') : Observable<any>{

   return this.http.put(this.baseUrl + extraLocation, resource );

  }






}
