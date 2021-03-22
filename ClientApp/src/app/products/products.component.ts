import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Product } from '../models/product';
import { GoodsService } from '../services/goods.service';
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
  
  product : Product[];
  control = new FormControl();
  filteredOptions: Observable<Array<any>>;

  constructor(private goodsService: GoodsService) { }

  ngOnInit(): void {
    //this.getProduct();
   
      this.goodsService.getProducts().subscribe(
        (res => {
          this.product = res/* .map(a =>a.name) */;
          console.log(this.product)  
        }))
        this.getUser();
      }






getUser(){
  this.filteredOptions = this.control.valueChanges
  .pipe(
    startWith(''),
    /*map(value => typeof value === 'string' ? value : value.name),
    map(name => name ? this._filter(name) : this.users.slice()),*/
    map((val) => this.filter(val))
    
  );
}

displayFn(user: User): any {
 console.log(user);

 return user && user.name ? user.name : ''; 
 // return user ? this.options.find(x => x.id === user).name : undefined;
 
  //return 'Hello';
}
filter(val: any): any {
 if (this.product !== undefined)
  return this.product.filter((item: any) => {
    //If the user selects an option, the value becomes a Human object,
    //therefore we need to reset the val for the filter because an
    //object cannot be used in this toLowerCase filter
    if (typeof val === 'object') { val = "" };
    const TempString = item.name //+ ' - ' + item.Surname;
    return TempString.toLowerCase().includes(val.toLowerCase());
  });
}
OnHumanSelected(option: MatOption) {
 console.log(option.value);
  console.log(this.control); //This has the correct data
  console.log(this.control.value); //Why is this different than the above result?
  console.log(this.product); //I want this to log the Selected Human Object
}

onChanged(){

}




}
