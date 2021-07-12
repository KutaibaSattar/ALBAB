import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, HostListener, OnInit, Self } from '@angular/core';
import {FormArray,FormBuilder,FormControl,FormGroup,Validators} from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DropDownValidators } from 'app/errors/dropdown.validators';
import { invoiceitemComponent } from 'app/invoiceitem/invoiceitem.component';
import { Member } from 'app/models/member';
import { Product } from 'app/models/product';
import { Invoice, invoicesList } from 'app/models/Invoice';
import { AuthService } from 'app/services/auth.service';
import { ConfirmService } from 'app/services/confirm.service';
import { DbAccountService } from 'app/services/dbaccount.service';
import { ProductService } from 'app/services/product.service';
import { InvoiceService } from 'app/services/Invoice.service';
import { ToastrService } from 'ngx-toastr';
import { forkJoin, Observable, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import {APP_CONFIG } from 'app/_helper/InvoiceType-token';

//AppConfig.apiEndpoint = 'purchase/';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss'],
  providers: [
    InvoiceService,
    {provide: APP_CONFIG, useValue: 'purchase/'},
   ],

})
export class PurchasesComponent implements OnInit {
  constructor(
    @Self() public purchaseService: InvoiceService,
    private productService: ProductService,
    private authService: AuthService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private dbAccountService : DbAccountService,
  ) {}
  /*  @HostListener('window:beforeunload', ['$event']) uloadNotification(
    $event: any
  ) {
    if (this.purchHdr.dirty) {
      $event.returnValue = true;
    }
  } */

  members : Member[];
  txtSearchInv: string ='';
  purchList: invoicesList[];
  formInvoice: FormGroup;
  appUserId: FormControl;
  dbAccountId: FormControl;
  invNo: FormControl;
  date: FormControl;
  invoiceId: FormControl;
  invDetails: FormArray;
  productId : FormControl[] = new Array();
  price : FormControl[] = new Array();
  quantity : FormControl[] = new Array();
  description : FormControl[] = new Array();
  grdTotal = new FormControl(''); // sepearated
  purchInv: Invoice = new Invoice();


  filteredPurchNos$: Observable<any>;

  totalSum: number[] = [];
  priceChanges$ = [];




  ngOnInit(): void {

    //this.productService.testing = true;

    this.purchaseService.getInvLists('purchListNo').pipe(
      map ((data : invoicesList[]) =>{
        this.purchList = data;

      })
    ).subscribe();

    this.initializeForm();
    //this.attachedUserFilter();
    this.attachItemFilter(0);
    this.listenToChanging(0);

    // initialized first time

    let sources = [
      this.authService.getMembers(),
      this.productService.getProducts(),
      this.dbAccountService.getFlattenDbAccounts()

    ];



    forkJoin(sources).subscribe(
      (data:any) =>{
        this.members = data[0];
      }
     );
  }

  initializeForm() {
    this.formInvoice = this.formBuilder.group({
      id: null,
      invNo: [null, Validators.required],
      appUserId: [null,[Validators.required, DropDownValidators.shouldLimited],],
      dbAccountId:[{value:null},[Validators.required, DropDownValidators.shouldLimited],],
      actionAcctId: null,
      date: [null, Validators.required],
      invDetails: this.formBuilder.array([this.initSection()]),
    });

    this.appUserId = this.formInvoice.get('appUserId') as FormControl;
    this.dbAccountId = this.formInvoice.get('dbAccountId') as FormControl;

    this.appUserId.valueChanges.subscribe( value =>{

        if ( typeof value == 'number'){
         this.dbAccountId.setValue( this.members.find(member => member.id == value).type,{emitEvent:false})
         this.dbAccountId.disable({emitEvent:false});
        }
        else
        this.dbAccountId.enable({emitEvent:false});

    })


    this.dbAccountId.valueChanges.subscribe((value) => {
      if (typeof value == 'number') {
        if (!this.members.find((member) => member.type == value)) {
          this.appUserId.setValue( null,{emitEvent:false})
          this.appUserId.disable({emitEvent:false});
        }
        else this.appUserId.enable({ emitEvent: false });
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
      productId:[null, [DropDownValidators.shouldLimited]],// [null, [Validators.required,DropDownValidators.shouldLimited]],
      description : null,
      price: [null, Validators.required],
      quantity: [null, Validators.required],
      unitTotalPrice: [{ value: '', disabled: true }],
    });
  }




  attachItemFilter(index: number) {

    this.productId[index] = this.invDetails.at(index).get('productId') as FormControl;
    this.price[index] = this.invDetails.at(index).get('price') as FormControl;
    this.quantity[index] = this.invDetails.at(index).get('quantity') as FormControl;
    this.description[index] = this.invDetails.at(index).get('description') as FormControl;


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

    this.invDetails.push(this.initSection());
    // Build the account Auto Complete values
    this.attachItemFilter(this.invDetails.length - 1);
    this.listenToChanging(this.invDetails.length - 1);
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


  filterPurchase() : Observable<any> {
    //if (this.purchNos)
    return  of(this.purchList?.filter((purch) =>
      purch.invNo.toLowerCase().includes(this.txtSearchInv)
    ));
  }




  PurchNameFn(option): any {
    //(this.purchNos) {
      return this.purchList?.find((element) => element.id === option).invNo;
      //return this.purchNos.
    //}
  }

  getPurch() {
    if (this.txtSearchInv) {
      this.purchaseService.getInvoice(this.txtSearchInv).subscribe(
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

  OnSubmit() {
    this.formInvoice.markAllAsTouched();

    if (this.formInvoice.valid) {
      this.purchInv =  this.formInvoice.getRawValue();


      //this.purchInv.purchDtl = this.invDetail.value;

      this.purchaseService.UpdaePurchInv(this.purchInv).subscribe(() => {
        this.toastr.success('Invoice updated successfully');
        this.formInvoice.markAsPristine();
      });
    }
  }

   removeUnit(i: number) {
    this.invDetails.removeAt(i);
    this.productId.splice(i,1);
    this.price.splice(i,1);
    this.quantity.splice(i,1);
    this.updateTotalUnitPrice( this.invDetails.controls.length-1);
  }

  clearInv() {
    if (this.formInvoice.dirty) {
      //return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
    }

    this.formInvoice.reset();
    this.invDetails.controls = [];
  }

  deleteInv() {
    this.confirmService.confirm('Confirm delete all invocie','This cannot be undone','Yes','No')
      .subscribe((result) => {
          if (result) {
            this.purchaseService
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
}
