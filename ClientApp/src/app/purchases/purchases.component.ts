import { Component, OnInit } from '@angular/core';
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
    private formBuilder : FormBuilder
  ) {}

  members: Member[];
  member: string;
  products: Product[];
  searchInv: string;
  purchInv: IPurchase = new IPurchase();
  filteredUsersOptions: Observable<Array<Member>>;
  filteredItemsOptions: Observable<Product[]>[] = [];



  purchHdr = this.formBuilder.group({
    id:null,
    purNo: [null,Validators.required],
    appUserId: [null,[Validators.required,DropDownValidators.shouldLimited]],
    purchDtl:this.formBuilder.array([]),
  });


  initSection() {
    return this.formBuilder.group({
      id: null,
      productId:[null,Validators.required,],
      price: [null,Validators.required],
      quantity: [null,Validators.required],
    });
  }


  appUserId =this.purchHdr.get('appUserId') as FormControl;
  purNo =this.purchHdr.get('purNo') as FormControl;

  purchDtl = this.purchHdr.get('purchDtl') as FormArray;


  ngOnInit(): void {
    //this.purchaseService.getPurchInv(1).subscribe(data => this.purchase = data)



    this.attachedUserFilter();

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

        this.purchHdr.patchValue({
          id: this.purchInv.id,
          purNo: this.purchInv.purNo,
          appUserId: this.purchInv.appUserId,
        });

        this.purchInv.purchDtl.map((item) => {
          const group = this.initSection();
          group.patchValue(item);
          (this.purchHdr.get('purchDtl') as FormArray).push(group);
          this.ManageNameControl(
            (this.purchHdr.get('purchDtl') as FormArray).controls.length - 1
          );
          //return group;
        });
      }
    });


  }

  attachedUserFilter(): any {
    this.filteredUsersOptions = this.purchHdr.get('appUserId').valueChanges.pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))
    );
  }

  someMethod(id: number) {
    return this.members.find((e) => (e.id = id)).displayName;
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
    console.log(option.value);
    console.log(this.appUserId); // This has the correct data
    console.log(this.appUserId.value); // Why is this different than the above result?
    console.log(this.members); // I want this to log the Selected Human Object
  }

  // tslint:disable-next-line: typedef
  OnSave(frmpurchase: IPurchase) {


    console.log(this.purchHdr)
   /*  this.purchaseService
      .UpdaePurchInv(frmpurchase)
      .subscribe((res) => console.log('close', res)); */
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
    const controls = <FormArray>this.purchHdr.get('purchDtl');
    controls.push(this.initSection());
    console.log('Array', controls);
    // Build the account Auto Complete values
    this.ManageNameControl(controls.length - 1);
  }

  getSections(form) {
    if ((form.controls.purchDtl as FormArray).controls.length > 0)
      return form.controls.purchDtl.controls;
  }




  ManageNameControl(index: number) {
    var arrayControl = this.purchHdr.get('purchDtl') as FormArray;

    this.filteredItemsOptions[index] = arrayControl
      .at(index)
      .get('productId')
      .valueChanges.pipe(
        startWith(''),
        /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
        map((val) => this._filter(val))
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

  OnSubmit(){
    this.purchHdr.markAllAsTouched()
    this.purchHdr["submitted"] = true;
    console.log(this.purchHdr)

  }
}
