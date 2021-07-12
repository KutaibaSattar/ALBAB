import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl } from '@angular/forms';

@Component({
  selector: 'invoice-template',
  templateUrl: './invoice-template.component.html',
  styleUrls: ['./invoice-template.component.scss']
})
export class InvoiceTemplateComponent implements OnInit {

 @Input() productId : FormControl
 @Input() price : FormControl
 @Input() quantity : FormControl
 @Input() description : FormControl



  constructor() { }

  ngOnInit(): void {
  }
  // removeUnit(i: number) {
  //   this.invDetails.removeAt(i);
  //   this.productId.splice(i,1);
  //   this.price.splice(i,1);
  //   this.quantity.splice(i,1);
  //   this.updateTotalUnitPrice( this.invDetails.controls.length-1);
  // }

}
