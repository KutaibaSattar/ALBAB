import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {



  constructor(private baseUrl: string, private http: HttpClient) { }

  getAll() {
    return this.http.get(this.baseUrl)

  }

  create(resource){
    return this.http.post(this.baseUrl , resource )

  }

  delete(id){
      return this.http.delete(this.baseUrl + id)
  }


}
