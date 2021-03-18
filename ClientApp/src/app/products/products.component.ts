import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { GoodsService } from '../_services/goods.service';
export interface User {
  name: string;
  id: number;
}
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  users: User[] = [
    {name: 'Mary', id: 1},
    {name: 'Shelley', id: 2},
    {name: 'Igor',id: 3}
  ];
  product : any;
  productName : string[];
  control = new FormControl();
  filtered$ : Observable<any>;
  filtered
  filteredOptions: Observable<Array<any>>;

  constructor(private goodsService: GoodsService) { }

  ngOnInit(): void {
    //this.getProduct();
    this.getUser();

}

getAll(){
 return this.control.valueChanges
   .pipe(startWith(''),
      map((value:any) => {
        let testing = this._filter(value)
        console.log('testing',testing);
        console.log(value);
        return testing;
        }),

    );
}

private _filter(value:string): /* string[] */User[] {

  const filterValue = value.toLowerCase();

  //return this.product.filter(option => option.toLowerCase().includes(filterValue));

  return this.users.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);

}



getProduct(){
this.goodsService.getProducts().subscribe(
  (res => {
    this.product = res.map(a =>a.name);
    this.filteredOptions =  this.getAll();

  })


)
}
getUser(){
  this.filteredOptions = this.control.valueChanges
  .pipe(
    startWith(''),
    map(value => typeof value === 'string' ? value : value.name),
    map(name => name ? this._filter(name) : this.users.slice())
  );
}

displayFn(user: number): string {

 /*  return user && user.name ? user.name : ''; */
 // return user ? this.options.find(x => x.id === user).name : undefined;
 if (user && this.users){
  this.users.find(x => x.id === user);
 }

  return 'Hello';
}


}
