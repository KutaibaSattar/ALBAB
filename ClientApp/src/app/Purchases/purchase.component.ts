import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Purchase } from '../models/purchase';
import { PurchaseItem } from '../models/purchase-item';
import { PurchaseService } from '../services/purchase.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit {

  purchase: Purchase;
  purchaseItems: PurchaseItem[];
  isValid = true;
  constructor(private purchaseService: PurchaseService ) {  }

  ngOnInit(): void {
    // tslint:disable-next-line: deprecation
    this.purchaseService.getPurchase().subscribe(
      (res => { console.log(res);

      }));

  }
  // tslint:disable-next-line: typedef
  AddOrEditPurchseItem(OrderItemIndex, OrderID) {}

  // tslint:disable-next-line: typedef
  OnDeletePurchseItem(purchItemId: number, i: number) {

  }

  // tslint:disable-next-line: typedef
  OnSubmit(form: NgForm) {}

}
