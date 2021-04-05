import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Purchase } from 'app/models/purchase';
import { PurchaseItem } from 'app/models/purchase-item';
import { AuthService } from 'app/services/auth.service';
import { PurchaseService } from 'app/services/purchase.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

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
    });}


  purchase: Purchase;
  purchaseItems: PurchaseItem[];
  members: Member[] ;
  member : string;

  isValid = true;

  filteredOptions: Observable<Array<Member>>;

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




   frm = new FormGroup({
    quantity: new FormControl('')});

  get InvItems (){
   return this.Invoice.get('InvItems') as FormArray
  }




  constructor(private purchaseService: PurchaseService, private authService: AuthService,private dialog: MatDialog) { }

  ngOnInit(): void {



    // tslint:disable-next-line: deprecation

    // tslint:disable-next-line: deprecation
    this.authService.getMembers().subscribe(
     members => {
     this.members = members;
     this.purchaseService.getPurchase().subscribe((result: any) => {
       if (result) {
         this.purchase = result;
         this.form.patchValue({
           purNo: this.purchase.purNo,
           appUserId: this.purchase.appUserId,
         });
         this.purchaseItems = result.purchDTLDtos;
         this.purchaseItems.map((item) => {
           const group = this.initSection();
           group.patchValue(item);
           (this.form.get('InvItems') as FormArray).push(group);
           console.log('New form', this.form);
           return group;
         });
       }
     });

    });
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
    this.filteredOptions = this.appUserId.valueChanges
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

  displayFn(id, _this) {
    let x = this.someMethod(_this.members[0].id) ;
    return x
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

      this.frm.registerControl('quantity',new FormControl(''))
      this.InvItems.push(this.frm);

    //this.skills.push(new FormControl(''));
    //(this.records as FormArray).push(this.formGroup);
   // (this.formChilds as FormArray).push(new FormControl(''))
  }

  getSections(form) {
   if( (form.controls.InvItems as FormArray).controls.length > 0)
    return form.controls.InvItems.controls;
  }








}
