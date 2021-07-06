import { HttpClient } from '@angular/common/http';
import { Component, Inject, Injectable, InjectionToken, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DropDownValidators } from 'app/errors/dropdown.validators';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Invoice, invoicesList } from 'app/models/purchase';
import { AuthService } from 'app/services/auth.service';
import { ConfirmService } from 'app/services/confirm.service';
import { DbAccountService } from 'app/services/dbaccount.service';
import { InvoiceService } from 'app/services/Invoice.service';
import { ProductService } from 'app/services/product.service';
import { AppConfig, APP_CONFIG} from 'app/_helper/tokens';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

function quotationServiceProvider(http: HttpClient): InvoiceService{
return new InvoiceService(http,'quote/');

}

const INVOICE_SERVICE = new InjectionToken<InvoiceService>('INVOICE_SERVICE');

@Component({
  selector: 'app-quotation',
  templateUrl: './quotation.component.html',
  providers: [
    {provide: APP_CONFIG, useValue: 'CONFIG'},
    {provide: INVOICE_SERVICE, useFactory: quotationServiceProvider, deps : [HttpClient]}
  ],
  styleUrls: ['./quotation.component.scss'],

})


export class QuotationComponent implements OnInit {
  constructor(
    @Inject(INVOICE_SERVICE) private quotationService :InvoiceService,
    private productService: ProductService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private dbAccountService: DbAccountService,
    private authService: AuthService,
    private dialog: MatDialog,
  ) {



  }

  members: Member[];
  txtSearchInv: string = '';
  qutLists: invoicesList[];
  lastNo : string;
  formInvoice: FormGroup;
  appUserId: FormControl;
  dbAccountId: FormControl;
  invNo: FormControl;
  date: FormControl;
  invoiceId: FormControl;
  invDetails: FormArray;
  productId: FormControl[] = new Array();
  price: FormControl[] = new Array();
  quantity: FormControl[] = new Array();
  grdTotal = new FormControl(''); // sepearated
  purchInv: Invoice = new Invoice();

  filteredPurchNos$: Observable<any>;

  totalSum: number[] = [];
  priceChanges$ = [];

  ngOnInit(): void {



      this.quotationService
      .getInvLists('quoteListNo')
      .pipe(
        map((data: invoicesList[]) => {
          this.qutLists = data;
          console.log(data);
        })
      )
      .subscribe();

    let sources = [
      this.authService.getMembers(),
      //this.quotationService.getLastList(),
       this.productService.getProducts(),
       this.dbAccountService.getFlattenDbAccounts(),
    ];

    forkJoin(sources).subscribe((data: any) => {
      this.members = data[0];
      this.lastNo = data[1];
    });

    this.initializeForm();
    //this.attachedUserFilter();
    this.attachItemFilter(0);
    this.listenToChanging(0);
  }
  initializeForm() {
    this.formInvoice = this.formBuilder.group({
      id: null,
      invNo: [null, Validators.required],
      appUserId: [
        null,
        [Validators.required, DropDownValidators.shouldLimited],
      ],
      dbAccountId: [
        { value: null },
        [Validators.required, DropDownValidators.shouldLimited],
      ],
      actionAcctId: null,
      date: [null, Validators.required],
      invDetails: this.formBuilder.array([this.initSection()]),
    });

    this.appUserId = this.formInvoice.get('appUserId') as FormControl;
    this.dbAccountId = this.formInvoice.get('dbAccountId') as FormControl;

    this.appUserId.valueChanges.subscribe((value) => {
      if (typeof value == 'number') {
        this.dbAccountId.setValue(
          this.members.find((member) => member.id == value).type,
          { emitEvent: false }
        );
        this.dbAccountId.disable({ emitEvent: false });
      } else this.dbAccountId.enable({ emitEvent: false });
    });

    this.dbAccountId.valueChanges.subscribe((value) => {
      if (typeof value == 'number') {
        if (!this.members.find((member) => member.type == value)) {
          this.appUserId.setValue(null, { emitEvent: false });
          this.appUserId.disable({ emitEvent: false });
        } else this.appUserId.enable({ emitEvent: false });
      }
    });

    this.invNo = this.formInvoice.get('invNo') as FormControl;
    this.date = this.formInvoice.get('date') as FormControl;
    this.invoiceId = this.formInvoice.get('id') as FormControl;
    this.invDetails = this.formInvoice.get('invDetails') as FormArray;
  }

  initSection(): FormGroup {
    return this.formBuilder.group({
      id: null,
      productId: [null, [DropDownValidators.shouldLimited]], // [null, [Validators.required,DropDownValidators.shouldLimited]],
      description: null,
      price: [null, Validators.required],
      quantity: [null, Validators.required],
      unitTotalPrice: [{ value: '', disabled: true }],
    });
  }
  addRecord() {

    this.invDetails.push(this.initSection());
    // Build the account Auto Complete values
    this.attachItemFilter(this.invDetails.length - 1);
    this.listenToChanging(this.invDetails.length - 1);
  }
  OnSubmit() {
    this.formInvoice.markAllAsTouched();

    if (this.formInvoice.valid) {
      this.purchInv =  this.formInvoice.getRawValue();


      //this.purchInv.purchDtl = this.invDetail.value;

      this.quotationService.UpdaePurchInv(this.purchInv).subscribe(() => {
        this.toastr.success('Invoice updated successfully');
        this.formInvoice.markAsPristine();
      });
    }
  }
  clearInv() {
    if (this.formInvoice.dirty) {
      //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
    }}

    deleteInv() {
      this.confirmService.confirm('Confirm delete all invocie','This cannot be undone','Yes','No')
        .subscribe((result) => {
            if (result) {
              this.quotationService
                .deletePurchase(this.invoiceId.value)
                .subscribe((resp) => {
                  this.toastr.success('Invoice deleted successfully');
                  this.formInvoice.reset();
                  this.invDetails.controls = [];
                  this.addRecord();
                });
            }
        });
    }

    PurchNameFn(option): any {
      //(this.purchNos) {
        return this.qutLists?.find((element) => element.id === option).invNo;
        //return this.purchNos.
      //}
    }

    filterPurchase() : Observable<any> {
      //if (this.purchNos)
      return  of(this.qutLists?.filter((purch) =>
        purch.invNo.toLowerCase().includes(this.txtSearchInv)
      ));
    }
    getQuotation() {
      if (this.txtSearchInv) {
        this.quotationService.getPurchInv(this.txtSearchInv).subscribe(
            (result) => {
              if (result){

                this.purchInv = result;
               this.formInvoice.patchValue({
                id: this.purchInv.id,
                invNo: this.purchInv.invNo,
                appUserId: this.purchInv.appUserId,
                dbAccountId:this.purchInv.dbAccountId,
                actionAcctId: this.purchInv.actionAcctId,
                date: new Date(this.purchInv.date)

              });
              }


            if (this.formInvoice.dirty) {
              //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
            }

            if (this.invDetails.length > 0) {
              this.invDetails.controls = []; // delete balnck one
            }

            this.purchInv.invDetails.map((item) => {
              const group = this.initSection();
              group.patchValue(item);

              let arrayLength = this.invDetails.controls.length;

              this.invDetails.push(group);

              this.listenToChanging(arrayLength);
              this.updateTotalUnitPrice(arrayLength);

              this.attachItemFilter(arrayLength);
              //return group;
            });
          });
      }
    }
    AddOrEditPurchseItem(OrderID) {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      dialogConfig.width = '50%';
      dialogConfig.data = { OrderID };

      this.dialog.open(invoiceitemComponent, dialogConfig).afterClosed().subscribe();
    }
    removeUnit(i: number) {
      this.invDetails.removeAt(i);
      this.productId.splice(i,1);
      this.price.splice(i,1);
      this.quantity.splice(i,1);
      this.updateTotalUnitPrice( this.invDetails.controls.length-1);
    }
    attachItemFilter(index: number) {

      this.productId[index] = this.invDetails.at(index).get('productId') as FormControl;
      this.price[index] = this.invDetails.at(index).get('price') as FormControl;
      this.quantity[index] = this.invDetails.at(index).get('quantity') as FormControl;


      /* this.filteredItems$[index] = arrayControl.at(index).get('productId')
        .valueChanges.pipe(startWith(''),map((val) => this._filter(val))); */
    }
    listenToChanging(index: number) {
      this.invDetails.at(index).get('price')
        .valueChanges.subscribe(() => this.updateTotalUnitPrice(index));

      this.invDetails.at(index).get('quantity')
        .valueChanges.subscribe(() => this.updateTotalUnitPrice(index));
    }
    private updateTotalUnitPrice(index: number) {
      this.invDetails.at(index).get('unitTotalPrice').setValue(
          this.invDetails.at(index).get('price').value *
            this.invDetails.at(index).get('quantity').value
        );

      this.totalSum.splice(index,1,this.invDetails.at(index).get('unitTotalPrice').value
      );

      this.grdTotal.setValue(this.invDetails.getRawValue()
          .reduce((sum, current) => sum + current.unitTotalPrice, 0)
      );
    }


}
