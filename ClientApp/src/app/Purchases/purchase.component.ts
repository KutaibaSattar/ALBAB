import { HttpClient } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { AuthService } from 'app/services/auth.service';
import { Observable, of, ReplaySubject } from 'rxjs';
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
  members: Member[] ;
  member: Member;
  isValid = true;
  supplier = new FormControl();
  filteredOptions: Observable<Array<Member>>;

  constructor(private purchaseService: PurchaseService, private authService: AuthService,private dialog: MatDialog) {  }

  ngOnInit(): void {

    // tslint:disable-next-line: deprecation
    this.purchaseService.getPurchase().subscribe(
      (result: any) => {
      if (result) {
     this.purchase = result;
     console.log('Purchase', this.purchase);
    }});
    // tslint:disable-next-line: deprecation
    this.authService.getMembers().subscribe(
      (result: any) => {
      if (result) {
     this.members = result;
     console.log(result);
     this.supplier.setValue({id: 1});

    }});
    /* this.authService.getMember(1).subscribe(

      (result: any) => {
       if (result){
        (this.member = result);
        return this.members ? this.members.find(x => x.id === 1 ).displayName : undefined;
       }

      }

    ); */

    this.getUser();




  }
  getUser(): any{
    this.filteredOptions = this.supplier.valueChanges
    .pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))

    );
  }
  displayFn(user: Member): any {


    if (user ){
     /*  this.authService.membersSource$.subscribe(res => member = res ); */
    return  user.displayName;


    }




    // return user && user.userId ? user.displayName + ' - ' + user.userId : '';
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


  // tslint:disable-next-line: typedef
  OnDeletePurchseItem(purchItemId: number, i: number) {

  }
  // tslint:disable-next-line: typedef
  OnHumanSelected(option: MatOption) {
    console.log(option.value);
    console.log(this.supplier); // This has the correct data
    console.log(this.supplier.value); // Why is this different than the above result?
    console.log(this.members); // I want this to log the Selected Human Object
   }

  // tslint:disable-next-line: typedef
  OnSubmit(form: NgForm) {}

  AddOrEditPurchseItem(OrderID) {

    const dialogConfig = new MatDialogConfig;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "50%";
    dialogConfig.data = { OrderID };

    this.dialog.open(invoiceitemComponent, dialogConfig).afterClosed().subscribe();

  }



}


