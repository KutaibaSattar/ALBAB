import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'environments/environment';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl = environment.apiUrl;

  public CurrentProductSource = new BehaviorSubject<Product[]>(null);
  public productSource$ = this.CurrentProductSource.asObservable();

  products : Product[];

  constructor(private http: HttpClient) {

  /*   if (this.CurrentProductSource.value === null)
    this.getProducts().subscribe(); */


   // this.CurrentProductSource.next(this.CurrentProductSource.value)


  }

  getProducts() : Observable<Product[]>  {
    return this.http.get<Product[]>(this.baseUrl + 'products/products').pipe(
      map((res) => {
        this.products = res;
        this.CurrentProductSource.next(this.products);
        return this.products;
      })
    );
  }
}
