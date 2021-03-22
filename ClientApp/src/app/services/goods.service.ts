import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'environments/environment';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class GoodsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProducts() : Observable<Product[]>{

  return this.http.get<Product[]>(this.baseUrl+'goods/products')

  }
}
