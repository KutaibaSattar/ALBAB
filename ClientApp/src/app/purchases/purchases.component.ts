import { CurrencyPipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DropDownValidators } from 'app/errors/dropdown.validators';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Product } from 'app/models/product';
import { IPurchase } from 'app/models/purchase';
import { AuthService } from 'app/services/auth.service';
import { ProductService } from 'app/services/product.service';
import { PurchaseService } from 'app/services/purchase.service';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';


@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss'],
})
export class PurchasesComponent implements OnInit {
  constructor(
    public purchaseService: PurchaseService,
    private productService: ProductService,
    private authService: AuthService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,

  ) {}
 /*  @HostListener('window:beforeunload', ['$event']) uloadNotification(
    $event: any
  ) {
    if (this.purchHdr.dirty) {
      $event.returnValue = true;
    }
  } */

  members: Member[];
  member: string;
  products: Product[];
  searchInv: string;
  purchInv: IPurchase = new IPurchase();
  filteredUsers$: Observable<Array<Member>>;
  filteredItems$: Observable<Array<Product>>[] = [];
  totalSum:number[] =[] ;
  priceChanges$ = [];
  grdTotal = new FormControl('');

  formPurchHdr = this.formBuilder.group({
    id: null,
    purNo: [null, Validators.required],
    appUserId: [null, [Validators.required, DropDownValidators.shouldLimited]],
    subFormPurchDtl: this.formBuilder.array([
      this.initSection()


    ]),
  });



  initSection() : FormGroup {
    return this.formBuilder.group({
      id: null,
      productId: [null, Validators.required],
      price: [null, Validators.required],
      quantity: [null, Validators.required],
      unitTotalPrice: [{ value: "", disabled: true }]
    });
  }

  appUserId = this.formPurchHdr.get('appUserId') as FormControl;
  purNo = this.formPurchHdr.get('purNo') as FormControl;

  purchDtl = this.formPurchHdr.get('subFormPurchDtl') as FormArray;

  ngOnInit(): void {
    //this.purchaseService.getPurchInv(1).subscribe(data => this.purchase = data)

    this.attachedUserFilter();
    this.attachItemFilter(0);
    this.listenToChanging(0);

    let sources = [
      this.authService.getMembers(),
      this.productService.getProducts(),
    ];

    if (this.purchInv.id)
      sources.push(this.purchaseService.getPurchInv(this.searchInv));

    forkJoin(sources).subscribe((data) => {
      (<any>this.members) = data[0];

      (<any>this.products) = data[1];

      if (this.purchInv.id) {
        (<any>this.purchInv) = data[2];

        this.formPurchHdr.patchValue({
          id: this.purchInv.id,
          purNo: this.purchInv.purNo,
          appUserId: this.purchInv.appUserId,
        });

        this.purchInv.purchDtl.map((item) => {
          const group = this.initSection();
          group.patchValue(item);
          (this.formPurchHdr.get('subFormPurchDtl') as FormArray).push(group);
          this.attachItemFilter(
            (this.formPurchHdr.get('subFormPurchDtl') as FormArray).controls.length - 1
          );
          //return group;
        });
      }
    });



  }

  attachedUserFilter(): any {
    this.filteredUsers$ = this.formPurchHdr
      .get('appUserId')
      .valueChanges.pipe(
        startWith(''),
        /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
        map((val) => this.filter(val))
      );
  }



  displayFn(this, user: number): string {
    if (user) {
      let x = this.members.find((element) => element.id === user).displayName;
      return x;
    }
  }
  ProductNameFn(this, product: number): string {
    if (product) {
      let x = this.products.find((element) => element.id === product).name;
      return x;
    }
  }

  filter(val: any): any {
    if (this.members !== undefined) {
      return this.members.filter((item: Member) => {
        // If the user selects an option, the value becomes a Human object,
        // therefore we need to reset the val for the filter because an
        // object cannot be used in this toLowerCase filter
        let x = typeof val;
        if (typeof val === 'string') {
          const TempString = item.displayName + ' - ' + item.userId;
          return TempString.toLowerCase().includes(val.toLowerCase());
        }
      });
    }
  }
  // tslint:disable-next-line: typedef

  // tslint:disable-next-line: typedef
  OnDeletePurchseItem(purchItemId: number, i: number) {}
  // tslint:disable-next-line: typedef


  OnHumanSelected(option: MatOption) {
   // console.log(option.value);
 }

  AddOrEditPurchseItem(OrderID) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';
    dialogConfig.data = { OrderID };

    this.dialog
      .open(invoiceitemComponent, dialogConfig)
      .afterClosed()
      .subscribe();
  }

  addRecord() {
    const controls = <FormArray>this.formPurchHdr.get('subFormPurchDtl');
    controls.push(this.initSection());
    // Build the account Auto Complete values
    this.attachItemFilter(controls.length - 1);
    this.listenToChanging(controls.length - 1)
  }

  getSections(form: FormGroup) {
    if ((form.controls.subFormPurchDtl as FormArray).controls.length > 0)
      return (<FormArray>form.controls.subFormPurchDtl).controls;
  }

  attachItemFilter(index: number) {
    var arrayControl = this.formPurchHdr.get('subFormPurchDtl') as FormArray;

    this.filteredItems$[index] = arrayControl
      .at(index)
      .get('productId')
      .valueChanges.pipe(
        startWith(''),
        /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
        map((val) => this._filter(val))
      );
  }

 listenToChanging(index: number) {

      this.purchDtl.at(index).get('price')
      .valueChanges.subscribe(units => this.updateTotalUnitPrice(units,index));

      this.purchDtl.at(index).get('quantity')
      .valueChanges.subscribe(units => this.updateTotalUnitPrice(units,index));


  }


  private _filter(val: any): Product[] {
    if (this.products !== undefined) {
      return this.products.filter((item: Product) => {
        // If the user selects an option, the value becomes a Human object,
        // therefore we need to reset the val for the filter because an
        // object cannot be used in this toLowerCase filter
        let x = typeof val;
        if (typeof val === 'string') {
          const TempString = item.name; //+ ' - ' + item.userId;
          return TempString.toLowerCase().includes(val.toLowerCase());
        }
      });
    }
  }

  OnSubmit() {
    this.formPurchHdr.markAllAsTouched();

    console.log(this.formPurchHdr.value);

    this.purchInv = this.formPurchHdr.value
    this.purchInv.id = 0;

    this.purchaseService.UpdaePurchInv(this.purchInv).subscribe(() => {
      console.log('close');
      this.toastr.success('Invoice updated successfully')
      this.formPurchHdr.reset(this.purchInv)
    });
  }

  private updateTotalUnitPrice(units: any, index: number) {

    this.purchDtl.at(index).get('unitTotalPrice').setValue
      (this.purchDtl.at(index).get('price').value * this.purchDtl.at(index).get('quantity').value)


      this.totalSum.splice(index,1, this.purchDtl.at(index).get('unitTotalPrice').value)
      console.log( this.totalSum.reduce((sum,current) => sum + current))

      console.log( this.purchDtl.getRawValue().reduce((sum,current) => sum + current.unitTotalPrice,0))
    this.grdTotal.setValue(this.purchDtl.getRawValue().reduce((sum,current) => sum + current.unitTotalPrice,0))

    /* if (this.totalSum.length <= index){
      total.push(index,index,this.purchDtl.at(index).get('unitTotalPrice').value)
    }
    else{
      total[index] = index,index,this.purchDtl.at(index).get('unitTotalPrice').value
    } */


    /* // get our units group controll
    const control = <FormArray>this.formPurchHdr.controls["units"];
    // before recount total price need to be reset.
    this.totalSum = 0;
    for (let i in units) {
      let totalUnitPrice = units[i].qty * units[i].unitPrice;
      // now format total price with angular currency pipe
      let totalUnitPriceFormatted = this.currencyPipe.transform(
        totalUnitPrice,
        "USD",
        "symbol-narrow",
        "1.2-2"
      );
      // update total sum field on unit and do not emit event myFormValueChanges$ in this case on units
      control
        .at(+i)
        .get("unitTotalPrice")
        .setValue(totalUnitPriceFormatted, {
          onlySelf: true,
          emitEvent: false
        });
      // update total price for all units
      this.totalSum += totalUnitPrice;
    } */
  }
  removeUnit(i: number) {

   this.purchDtl.removeAt(i);
  }
}
