import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss']
})
export class DateInputComponent implements ControlValueAccessor,OnInit {
  @Input() label:string;
  @Input() maxDate:Date;
  bsConfig : Partial<BsDatepickerConfig>;
  defaultDate: Date = new Date()

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'DD/MM/YYYY',
      isAnimated: true,

    }
   }

   ngOnInit(): void {
   this.control.setValue(this.defaultDate);
  }

  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  get control(){
    return this.ngControl.control as FormControl

 }
 dateCreated($event){
      return this.defaultDate;
    }




}
