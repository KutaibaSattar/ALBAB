import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-invitems',
  templateUrl: './invoiceitem.component.html',
  styleUrls: ['./invoiceitem.component.scss']
})
export class invoiceitemComponent implements OnInit {

  form = new FormGroup({
    item: new FormControl(0,Validators.required),
    quantity: new FormControl(0,Validators.required),
  }
  );



  constructor() { }

  ngOnInit(): void {
  }

 // property
  get quantityProp(){
    return this.form.get('quantity')

  }

}
