import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import {FormArray,FormBuilder,FormControl,FormGroup,Validators} from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DropDownValidators } from 'app/errors/dropdown.validators';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Product } from 'app/models/product';
import { IPurchase } from 'app/models/purchase';
import { AuthService } from 'app/services/auth.service';
import { ConfirmService } from 'app/services/confirm.service';
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
    private datePipe: DatePipe,
    private confirmService: ConfirmService
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
  txtSearchInv: string;
  purchNos: [{ id: number; purNo: string }];
  formPurchHdr: FormGroup;
  appUserId: FormControl;
  purNo: FormControl;
  purDate: FormControl;
  invoiceId: FormControl;
  purchDtl: FormArray;

  purchInv: IPurchase = new IPurchase();

  filteredUsers$: Observable<Array<Member>>;
  filteredItems$: Observable<Array<Product>>[] = [];
  filteredPurchNos$: Observable<any>;

  totalSum: number[] = [];
  priceChanges$ = [];
  grdTotal = new FormControl('');



  ngOnInit(): void {
    //this.purchaseService.getPurchInv(1).subscribe(data => this.purchase = data)

    this.initializeForm();
    this.attachedUserFilter();
    this.attachItemFilter(0);
    this.listenToChanging(0);

    let sources = [
      this.authService.getMembers(),
      this.productService.getProducts(),
    ];

    forkJoin(sources).subscribe((data) => {
      (<any>this.members) = data[0];

      (<any>this.products) = data[1];
    });
  }

  initializeForm() {
    this.formPurchHdr = this.formBuilder.group({
      id: 0,
      purNo: [null, Validators.required],
      appUserId: [null,[Validators.required, DropDownValidators.shouldLimited],],
      purDate: [null, Validators.required],
      subFormPurchDtl: this.formBuilder.array([this.initSection()]),
    });
    this.appUserId = this.formPurchHdr.get('appUserId') as FormControl;
    this.purNo = this.formPurchHdr.get('purNo') as FormControl;
    this.purDate = this.formPurchHdr.get('purDate') as FormControl;
    this.invoiceId = this.formPurchHdr.get('id') as FormControl;
    this.purchDtl = this.formPurchHdr.get('subFormPurchDtl') as FormArray;
  }

  initSection(): FormGroup {
    return this.formBuilder.group({
      id: 0,
      productId: [null, Validators.required],
      price: [null, Validators.required],
      quantity: [null, Validators.required],
      unitTotalPrice: [{ value: '', disabled: true }],
    });
  }


  attachedUserFilter(): any {
    this.filteredUsers$ = this.formPurchHdr.get('appUserId').valueChanges.pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))
    );
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

  OnHumanSelected(option: MatOption) {
    console.log(option.value);
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
    this.listenToChanging(controls.length - 1);
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
    this.purchDtl
      .at(index)
      .get('price')
      .valueChanges.subscribe((units) => this.updateTotalUnitPrice(index));

    this.purchDtl
      .at(index)
      .get('quantity')
      .valueChanges.subscribe((units) => this.updateTotalUnitPrice(index));
  }

  private updateTotalUnitPrice(index: number) {
    this.purchDtl
      .at(index)
      .get('unitTotalPrice')
      .setValue(
        this.purchDtl.at(index).get('price').value *
          this.purchDtl.at(index).get('quantity').value
      );

    this.totalSum.splice(
      index,
      1,
      this.purchDtl.at(index).get('unitTotalPrice').value
    );
    console.log(this.totalSum.reduce((sum, current) => sum + current));

    console.log(
      this.purchDtl
        .getRawValue()
        .reduce((sum, current) => sum + current.unitTotalPrice, 0)
    );
    this.grdTotal.setValue(
      this.purchDtl
        .getRawValue()
        .reduce((sum, current) => sum + current.unitTotalPrice, 0)
    );
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

  doFilter() {
    this.filteredPurchNos$ = this.purchaseService
      .getPurchNos()
      .pipe(map((jokes) => this.filterPurchase(jokes)));
  }
  filterPurchase(values) {
    console.log(values);
    return (this.purchNos = values.filter((joke) =>
      joke.purNo.toLowerCase().includes(this.txtSearchInv)
    ));
  }

  displayFn(this, user: number): string {
    if (user) {
      let x = this.members.find((element) => element.id === user).displayName;
      return x;
    }
  }
  ProductNameFn(this, product: number): string {
    if (product) {
      return this.products.find((element) => element.id === product).name;
    }
  }

  PurchNameFn(option): any {
    if (this.purchNos) {
      return this.purchNos.find((element) => element.id === option).purNo;
      //return this.purchNos.
    }
  }

  getPurch() {
    if (this.txtSearchInv) {
      this.purchaseService
        .getPurchInv(this.txtSearchInv)
        .subscribe((result) => {
          this.purchInv = result;
          this.formPurchHdr.patchValue({
            id: this.purchInv.id,
            purNo: this.purchInv.purNo,
            appUserId: this.purchInv.appUserId,
            purDate: this.datePipe.transform(
              this.purchInv.purDate,
              'yyyy-MM-dd'
            ),
          });

          if (this.formPurchHdr.dirty) {
            //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
          }

          if (this.purchDtl.length > 0) {
            this.purchDtl.controls = []; // delete balnck one
          }

          this.purchInv.purchDtl.map((item) => {
            const group = this.initSection();
            group.patchValue(item);

            let arrayLength = (this.formPurchHdr.get(
              'subFormPurchDtl'
            ) as FormArray).controls.length;

            (this.formPurchHdr.get('subFormPurchDtl') as FormArray).push(group);

            this.listenToChanging(arrayLength);
            this.updateTotalUnitPrice(arrayLength);

            this.attachItemFilter(arrayLength);
            //return group;
          });
        });
    }
  }

  OnSubmit() {
    this.formPurchHdr.markAllAsTouched();

    if (this.formPurchHdr.valid) {
      this.purchInv = this.formPurchHdr.value;
      this.purchInv.purchDtl = this.purchDtl.value;

      this.purchaseService.UpdaePurchInv(this.purchInv).subscribe(() => {
        this.toastr.success('Invoice updated successfully');
        this.formPurchHdr.markAsPristine();
      });
    }
  }

  removeUnit(i: number) {
    this.purchDtl.removeAt(i);
  }

  NewInv() {
    if (this.formPurchHdr.dirty) {
      //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
    }

    this.formPurchHdr.reset();
  }

  deleteInv() {
    this.confirmService
      .confirm(
        'Confirm delete all invocie',
        'This cannot be undone',
        'Yes',
        'No'
      )
      .subscribe((result) => {
        if (result) {
          if (result) {
            this.purchaseService
              .deletePurchase(this.invoiceId.value)
              .subscribe((resp) => {
                this.toastr.success('Invoice deleted successfully');
                this.formPurchHdr.reset();
                this.purchDtl.controls = [];
                this.addRecord();
              });
          }
        }
      });
  }
}
