import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {



  constructor(private baseUrl: string, private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  getAll() {
    return this.http.get(this.baseUrl);

  }

  // tslint:disable-next-line: typedef
  create(resource){
    return this.http.post(this.baseUrl , resource );

  }

  // tslint:disable-next-line: typedef
  delete(id){
      return this.http.delete(this.baseUrl + id);
  }


}
