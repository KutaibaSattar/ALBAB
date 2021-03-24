import { Component, OnInit } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { Member } from 'app/models/member';
import { AuthService } from 'app/services/auth.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
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
  members: Member[];
  isValid = true;
  control = new FormControl();
  filteredOptions: Observable<Array<Member>>;
  constructor(private purchaseService: PurchaseService, private authService: AuthService ) {  }

  ngOnInit(): void {

    // tslint:disable-next-line: deprecation
    this.purchaseService.getPurchase().subscribe(
      (result: any) => {
      if (result) {
     this.purchase = result;
    }});
    // tslint:disable-next-line: deprecation
    this.authService.getMembers().subscribe(
      (result: any) => {
      if (result) {
     this.members = result;
     console.log(result);
    }});
    this.getUser();


  }
  getUser(): any{
    this.filteredOptions = this.control.valueChanges
    .pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))

    );
  }
  displayFn(user: Member): any {
    console.log(user);

    return user && user.userId ? user.displayName + ' - ' + user.userId : '';
    // return user ? this.options.find(x => x.id === user).name : undefined;

     // return 'Hello';
   }
  filter(val: any): any {
    if (this.members !== undefined) {
     return this.members.filter((item: Member) => {
       // If the user selects an option, the value becomes a Human object,
       // therefore we need to reset the val for the filter because an
       // object cannot be used in this toLowerCase filter
       if (typeof val === 'object') { val = ''; }
       const TempString = item.displayName + ' - ' + item.userId;
       return TempString.toLowerCase().includes(val.toLowerCase());

     });
    }
   }
  // tslint:disable-next-line: typedef
  AddOrEditPurchseItem(OrderItemIndex, OrderID) {}

  // tslint:disable-next-line: typedef
  OnDeletePurchseItem(purchItemId: number, i: number) {

  }
  OnHumanSelected(option: MatOption) {
    console.log(option.value);
    console.log(this.control); // This has the correct data
    console.log(this.control.value); // Why is this different than the above result?
    console.log(this.members); // I want this to log the Selected Human Object
   }

  // tslint:disable-next-line: typedef
  OnSubmit(form: NgForm) {}

}
