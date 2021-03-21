import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Purchase } from '../_models/purchase';
import { PurchaseItem } from '../_models/purchase-item';
import { PurchaseService } from '../_services/purchase.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit {

  purchase : Purchase;
  purchaseItems : PurchaseItem[];
  isValid: boolean = true;
  constructor(private purchaseService : PurchaseService ) {  }

  ngOnInit(): void {
    this.purchaseService.getAll().subscribe(
      (res => {
        console.log(res);
      }));

  }
  AddOrEditPurchseItem(OrderItemIndex, OrderID) {}

  OnDeletePurchseItem(purchItemId: number, i: number) {

  }

  OnSubmit(form: NgForm) {}

}
