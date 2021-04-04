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






  purchase: Purchase;
  purchaseItems: PurchaseItem[];
  members: Member[] ;
  member: Member;
  isValid = true;

  filteredOptions: Observable<Array<Member>>;



  Invoice = new FormGroup({

    purNo: new FormControl(''),
    appUserId : new FormControl(''),

    //InvItems: new FormArray([])


  });

  appUserId= this.Invoice.get('appUserId') as FormControl;


   frm = new FormGroup({
    quantity: new FormControl('')});

  get InvItems (){
   return this.Invoice.get('InvItems') as FormArray
  }




  constructor(private purchaseService: PurchaseService, private authService: AuthService,private dialog: MatDialog) { }

  ngOnInit(): void {

    // tslint:disable-next-line: deprecation
    this.purchaseService.getPurchase().subscribe(
      (result: any) => {
      if (result) {
     this.purchase = result;
    this.purchaseItems = result.purchDTLDtos;
     const controls = this.purchaseItems.map(items => {
      console.log(items);
      return new FormControl(items, Validators.required);

    });

    controls.forEach( (element) => {
      let x = Object.keys(element.value);
      console.log(x[0]);
  });




    this.Invoice.registerControl('InvItems',new FormArray(controls))

     console.log('Purchase', this.purchase);
     console.log('Invoice', this.Invoice);
    }});
    // tslint:disable-next-line: deprecation
    this.authService.getMembers().subscribe(
      (result: any) => {
      if (result) {
     this.members = result;
     console.log('members', this.members);
     this.getUser();
    }});
    /* this.authService.getMember(1).subscribe(

      (result: any) => {
       if (result){
        (this.member = result);
        return this.members ? this.members.find(x => x.id === 1 ).displayName : undefined;
       }

      }

    ); */






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








}
