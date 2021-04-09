import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {



  constructor(private baseUrl: string, protected http: HttpClient) { }

  // tslint:disable-next-line: typedef
 protected getAll(extraLocation: string ='') {
    return this.http.get(this.baseUrl + extraLocation);

  }

  // tslint:disable-next-line: typedef
  protected create(resource){
    return this.http.post(this.baseUrl , resource );

  }

  // tslint:disable-next-line: typedef
  protected delete(id){
      return this.http.delete(this.baseUrl + id);
  }

  protected getById(id,extraLocation: string ='') : Observable<any>{

   return this.http.get(this.baseUrl + extraLocation + id);

  }


}
