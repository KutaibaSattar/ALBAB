import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GoodsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProducts() : Observable<any>{

  return this.http.get(this.baseUrl+'goods/products')

  }
}
