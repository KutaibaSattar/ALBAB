import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import {FormArray,FormBuilder,FormControl,FormGroup,Validators} from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DropDownValidators } from 'app/errors/dropdown.validators';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Product } from 'app/models/product';
import { IInvoice } from 'app/models/purchase';
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
  listsFilterCustomer : any
  listsFilterProducts : any

  members: Member[];
  member: string;
  products: Product[];
  txtSearchInv: string;
  purchNos: [{ id: number; invNo: string }];
  formInvoice: FormGroup;
  appUserId: FormControl;
  invNo: FormControl;
  date: FormControl;
  invoiceId: FormControl;
  invDetail: FormArray;
  productId : FormControl[] = new Array();

  grdTotal = new FormControl(''); // sepearated

  purchInv: IInvoice = new IInvoice();

  //filteredUsers$: Observable<Array<Member>>;
  //filteredItems$: Observable<Array<Product>>[] = [];
  filteredPurchNos$: Observable<any>;

  totalSum: number[] = [];
  priceChanges$ = [];




  ngOnInit(): void {
    //this.purchaseService.getPurchInv(1).subscribe(data => this.purchase = data)

    this.initializeForm();
    //this.attachedUserFilter();
    this.attachItemFilter(0);
    this.listenToChanging(0);

    let sources = [
      this.authService.getMembers(),
      this.productService.getProducts(),
    ];

    forkJoin(sources).subscribe((data) => {
      (<any>this.members) = data[0];

      this.listsFilterCustomer = this.members.map(obj =>{
        var returnObj = {};
        const mapping = ['id', 'name','keyId'];
        returnObj[mapping[0]] = obj.id;
        returnObj[mapping[1]] = obj.name;
        returnObj[mapping[2]] = obj.keyId;

        return returnObj;
     });



      (<any>this.products) = data[1];

      this.listsFilterProducts = this.products.map(obj =>{
        var returnObj = {};
        const mapping = ['id', 'name'];
        returnObj[mapping[0]] = obj.id;
        returnObj[mapping[1]] = obj.name;
        return returnObj;
     });


    });
  }

  initializeForm() {
    this.formInvoice = this.formBuilder.group({
      id: 0,
      invNo: [null, Validators.required],
      appUserId: [null,[Validators.required, DropDownValidators.shouldLimited],],
      accountId:0,
      date: [null, Validators.required],
      invDetails: this.formBuilder.array([this.initSection()]),
    });
    this.appUserId = this.formInvoice.get('appUserId') as FormControl;
    this.invNo = this.formInvoice.get('invNo') as FormControl;
    this.date = this.formInvoice.get('date') as FormControl;
    this.invoiceId = this.formInvoice.get('id') as FormControl;
    this.invDetail = this.formInvoice.get('invDetails') as FormArray;

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




  attachItemFilter(index: number) {

    this.productId[index] = (<FormArray>this.formInvoice.get('invDetails')).at(index).get('productId') as FormControl

    /* this.filteredItems$[index] = arrayControl.at(index).get('productId')
      .valueChanges.pipe(startWith(''),map((val) => this._filter(val))); */
  }

/*   private _filter(val: any): Product[] {
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
  } */

  OnHumanSelected(option: MatOption) {
    console.log(option.value);
  }

  AddOrEditPurchseItem(OrderID) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';
    dialogConfig.data = { OrderID };

    this.dialog.open(invoiceitemComponent, dialogConfig).afterClosed().subscribe();
  }



  addRecord() {
    const controls = <FormArray>this.formInvoice.get('invDetails');
    controls.push(this.initSection());
    // Build the account Auto Complete values
    this.attachItemFilter(controls.length - 1);
    this.listenToChanging(controls.length - 1);
  }



  getSections(form: FormGroup) {
    if ((form.controls.invDetails as FormArray).controls.length > 0)
      return (<FormArray>form.controls.invDetails).controls;
  }



  listenToChanging(index: number) {
    this.invDetail.at(index).get('price')
      .valueChanges.subscribe((units) => this.updateTotalUnitPrice(index));

    this.invDetail.at(index).get('quantity')
      .valueChanges.subscribe((units) => this.updateTotalUnitPrice(index));
  }

  private updateTotalUnitPrice(index: number) {
    this.invDetail.at(index).get('unitTotalPrice').setValue(
        this.invDetail.at(index).get('price').value *
          this.invDetail.at(index).get('quantity').value
      );

    this.totalSum.splice(index,1,this.invDetail.at(index).get('unitTotalPrice').value
    );

    this.grdTotal.setValue(this.invDetail.getRawValue()
        .reduce((sum, current) => sum + current.unitTotalPrice, 0)
    );
  }



  doFilter() {
    this.filteredPurchNos$ = this.purchaseService.getPurchNos()
      .pipe(map((jokes) => this.filterPurchase(jokes)));
  }
  filterPurchase(values) {
    return (this.purchNos = values.filter((joke) =>
      joke.invNo.toLowerCase().includes(this.txtSearchInv)
    ));
  }

 /*  displayFn(this, user: number): string {
    if (user) {
      let x = this.members.find((element) => element.id === user).name;
      return x;
    }
  }
  ProductNameFn(this, product: number): string {
    if (product) {
      return this.products.find((element) => element.id === product).name;
    }
  } */

  PurchNameFn(option): any {
    if (this.purchNos) {
      return this.purchNos.find((element) => element.id === option).invNo;
      //return this.purchNos.
    }
  }

  getPurch() {
    if (this.txtSearchInv) {
      this.purchaseService.getPurchInv(this.txtSearchInv).subscribe(
          (result) => {
          this.purchInv = result;
          this.formInvoice.patchValue({
            id: this.purchInv.id,
            invNo: this.purchInv.invNo,
            appUserId: this.purchInv.appUserId,
            date: this.datePipe.transform(this.purchInv.date,'yyyy-MM-dd'),
          });

          if (this.formInvoice.dirty) {
            //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
          }

          if (this.invDetail.length > 0) {
            this.invDetail.controls = []; // delete balnck one
          }

          this.purchInv.invDetails.map((item) => {
            const group = this.initSection();
            group.patchValue(item);

            let arrayLength = (this.formInvoice.get('invDetails') as FormArray).controls.length;

            (this.formInvoice.get('invDetails') as FormArray).push(group);

            this.listenToChanging(arrayLength);
            this.updateTotalUnitPrice(arrayLength);

            this.attachItemFilter(arrayLength);
            //return group;
          });
        });
    }
  }

  OnSubmit() {
    this.formInvoice.markAllAsTouched();

    if (this.formInvoice.valid) {
      this.purchInv = this.formInvoice.value;
      //this.purchInv.purchDtl = this.invDetail.value;

      this.purchaseService.UpdaePurchInv(this.purchInv).subscribe(() => {
        this.toastr.success('Invoice updated successfully');
        this.formInvoice.markAsPristine();
      });
    }
  }

  removeUnit(i: number) {
    this.invDetail.removeAt(i);
  }

  clearInv() {
    if (this.formInvoice.dirty) {
      //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
    }

    this.formInvoice.reset();
  }

  deleteInv() {
    this.confirmService.confirm('Confirm delete all invocie','This cannot be undone','Yes','No')
      .subscribe((result) => {
        if (result) {
          if (result) {
            this.purchaseService
              .deletePurchase(this.invoiceId.value)
              .subscribe((resp) => {
                this.toastr.success('Invoice deleted successfully');
                this.formInvoice.reset();
                this.invDetail.controls = [];
                this.addRecord();
              });
          }
        }
      });
  }
}
