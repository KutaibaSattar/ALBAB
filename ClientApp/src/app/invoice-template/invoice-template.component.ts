import { Component, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormControl } from '@angular/forms';
import { EventEmitter } from '@angular/core';

@Component({
  selector: '[invoice-template-tr]',
  templateUrl: './invoice-template.component.html',
  styleUrls: ['./invoice-template.component.scss']
})
export class InvoiceTemplateComponent implements OnInit {
 @Input('recordIndex') i : number;
 @Input() productId : FormControl
 @Input() price : FormControl
 @Input() quantity : FormControl
 @Input() description : FormControl
 @Input() unitTotalPrice : FormControl

 @Output() deleteItem = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onDeleteItem(event,i){

    this.deleteItem.emit({event,i});

  }

}
