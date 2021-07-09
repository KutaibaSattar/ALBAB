import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { MatOption } from '@angular/material/core';
import { Member } from 'app/models/member';
import { Product } from 'app/models/product';
import { AuthService } from 'app/services/auth.service';
import { DbAccountService } from 'app/services/dbaccount.service';
import { ProductService } from 'app/services/product.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-dropown-template',
  templateUrl: './dropown-template.component.html',
  styleUrls: ['./dropown-template.component.scss']
})
export class DropownTemplateComponent implements OnInit {

  filtered$: Observable<Array<any>>;
  @Input() controlName : FormControl;
  @Input() listsFilter : any [];
  @Input()label: string ='';
  @Input() members  = false;
  @Input() products= false;
  @Input() accounts = false;
  @Input() textDisabled = false;
  showing: boolean;

  constructor(private memberService : AuthService, private productService : ProductService,
     private accountService : DbAccountService) { }


  ngOnInit(): void {


    if (this.members)
       this.memberService.membersSource$.pipe(
       map ((members:Member[]) => {
        this.listsFilter =   members.map( obj =>{
           var returnObj = {};
           const mapping = ['id', 'name','keyId'];
           returnObj[mapping[0]] = obj.id;
           returnObj[mapping[1]] = obj.name;
           returnObj[mapping[2]] = obj.keyId;
            return returnObj;
           })

           this.attachedFilter();
       this.showing = true;
       })
     ).subscribe();

  if(this.accounts)
      this.accountService.dbAccountService$.subscribe(
        res =>{
          if (res){
            this.listsFilter = res;
            this.showing = true;
            this.attachedFilter();
          }
        }
       )


  if (this.products)
        this.productService.productsSource$.subscribe(
       res => {
        if (res){
           this.listsFilter = res;
           this.showing = true;
           this.attachedFilter();
        }
        }

      )

      // this.productService.getProducts().pipe(
      //   map((product: Product[]) => {
      //     console.log('Product', product);
      //     this.listsFilter = product;
      //     this.showing = true;
      //   })
      // )
      // .subscribe();


  }

  attachedFilter(): any {

    this.filtered$ = this.controlName.valueChanges.pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))
    );
  }



  filter(val: any) {
    if (this.listsFilter != undefined) {
    return  this.listsFilter.filter((item:any) => {
        // If the user selects an option, the value becomes a Human object,
        // therefore we need to reset the val for the filter because an
        // object cannot be used in this toLowerCase filter
        let x = typeof val;
        if (typeof val === 'string') {
          const TempString = item.name + ' - ' + item.keyId;
          return  TempString.toLowerCase().includes(val.toLowerCase());
        }
      });
    }
  }

  displayFn(item: number): string {
    if ( item >0 && this.listsFilter) {
      let IdName = this.listsFilter.find((element) => element.id === item).name;
      console.log('Name',IdName)
      return IdName;
    }
  }

  // filterTesting(){
  //   this.controlName.valueChanges.pipe(
  //     //startWith(''),
  //     /*map(value => typeof value === 'string' ? value : value.name),
  //     map(name => name ? this._filter(name) : this.users.slice()),*/
  //    map((val) => {
  //       console.log(val);
  //       //return this.filter(val);
  //        return  this.memberService.getMembers().pipe(
  //           map( x=> {
  //             console.log('First',x);
  //              return  x.filter(item => item.name.toLowerCase().includes(val))

  //           })
  //       )

  //     }),

  //   ).subscribe( x => {console.log('Output',x)
  //     this.filtered$ = x;

  //   })
  // }

}
