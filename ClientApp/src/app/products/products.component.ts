import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { GoodsService } from '../_services/goods.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  product : any;
  productName : string[];
  control = new FormControl();
  filteredStreets: Observable<Array<any>>;

  constructor(private goodsService: GoodsService) { }

  ngOnInit(): void {
    this.goodsService.getProducts().subscribe(
      (res: any) => {
     this.product = res;
     this.productName = res.map(a => a.name);

     this.filteredStreets = this.control.valueChanges.pipe(
      startWith(''),
      map(name => this._filter(name))


    );
    this.filteredStreets.subscribe( res => console.log('filter',res));


    }


    );




   //this.streets = ['Champs-Élysées', 'Lombard Street', 'Abbey Road', 'Fifth Avenue'];
  }
  private _filter(value: string): string[] {
    const filterValue = this._normalizeValue(value);
    return this.productName.filter(street => this._normalizeValue(street).includes(filterValue));
  }

  private _normalizeValue(value: string): string {
    return value.toLowerCase().replace(/\s/g, '');
  }


}
