import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Product } from 'app/models/product';
import { Purchase } from 'app/models/purchase';
import { PurchaseItem } from 'app/models/purchase-item';
import { AuthService } from 'app/services/auth.service';
import { GoodsService } from 'app/services/goods.service';
import { PurchaseService } from 'app/services/purchase.service';
import { forkJoin, observable, Observable } from 'rxjs';
import { map, observeOn, startWith } from 'rxjs/operators';


@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss']
})
export class PurchasesComponent implements OnInit {






  initSection() {
    return new FormGroup({
      productId: new FormControl(''),
      price: new FormControl(''),
      quantity: new FormControl('')
    }

    );


  }


  purchase: Purchase;
  purchaseItems: PurchaseItem[];
  members: Member[] ;
  member : string;
  products : Product[];

  isValid = true;

  customeOptions: Observable<Array<Member>>;

  goodsOptions: Observable<Array<Member>>;


  form = new FormGroup({
    purNo: new FormControl(''),
    appUserId : new FormControl(''),
    InvItems : new FormArray([])

  })



  Invoice = new FormGroup({

    purNo: new FormControl(''),
    appUserId : new FormControl(''),

    //InvItems: new FormArray([])


  });

  appUserId= this.form.get('appUserId') as FormControl;
  InvItems =this.Invoice.get('InvItems') as FormArray;
  productId: FormControl;








  constructor(private purchaseService: PurchaseService,private goodsService: GoodsService, private authService: AuthService,private dialog: MatDialog) { }

  ngOnInit(): void {
   var sources = [
     this.authService.getMembers(),
     this.purchaseService.getPurchase(),
     this.goodsService.getProducts(),
   ];

   forkJoin(sources).subscribe(data => {

      this.members = data[0];

      this.purchase = data[1];
      this.form.patchValue({
        purNo: this.purchase.purNo,
        appUserId: this.purchase.appUserId,
      });
      this.purchaseItems = data[1].purchDTLDtos;
        this.purchaseItems.map((item) => {
          const group = this.initSection();
          group.patchValue(item);
          (this.form.get('InvItems') as FormArray).push(group);


          //return group;
        });



   });

  this.getUser();
  this.getProducts();



  }
  getProducts(): any{
    this.goodsOptions = this.InvItems.get('productId').valueChanges
    .pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))

    );

  }

  getUser(): any{
    this.customeOptions = this.form.get('appUserId').valueChanges
    .pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))

    );
  }

  someMethod(id: number){
   return this.members.find( e => e.id = id).displayName;

  }

  /* displayFn(id, _this) {
    let x = this.someMethod(_this.members[0].id) ;
    return x
  } */

  displayFn(this,user: number): string {

    let x = this.members.find(element => element.id === user).displayName;
    return x
    //return user && user.displayName ? user.displayName : '';
  }
  /* displayFn = value => {
    // now you have access to 'this'
    this.someMethod(value.id);
    return this.member;
  }
 */
 /*  displayFn(_this): any {

    if (1 ){
       this.authService.membersSource$.subscribe(res => member = res );
    return _this.members.find(element => element.id = this.appUserId.value).displayName;


    //return userId.displayName
    }
    // return user && user.userId ? user.displayName + ' - ' + user.userId : '';
    // return user ? this.options.find(x => x.id === user).name : undefined;

     // return 'Hello';
   }
 */


  filter(val: any): any {
    if (this.members !== undefined) {
     return this.members.filter((item: Member) => {
       // If the user selects an option, the value becomes a Human object,
       // therefore we need to reset the val for the filter because an
       // object cannot be used in this toLowerCase filter
      let x =typeof val;
       if (typeof val === 'string'){
        const TempString = item.displayName + ' - ' + item.userId;
       return TempString.toLowerCase().includes(val.toLowerCase());}


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
    console.log(this.appUserId); // This has the correct data
    console.log(this.appUserId.value); // Why is this different than the above result?
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

  addRecord() {

    const control = <FormArray>this.form.get('InvItems');
    control.push(this.initSection());

  }

  getSections(form) {
   if( (form.controls.InvItems as FormArray).controls.length > 0)
    return form.controls.InvItems.controls;
  }








}
