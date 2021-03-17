import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GoodsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProducts(){

  return  this.http.get(this.baseUrl+'goods/products');

  }
}
