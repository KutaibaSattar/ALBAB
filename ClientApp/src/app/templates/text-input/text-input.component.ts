import { Component, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { _MatAutocompleteBase } from '@angular/material/autocomplete';


@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label: string;
  @Input() type = 'text';
  @Input() myAutoComplete: any;
  @Input() autoCompleteDisabled: boolean = true ;
  //@Input('matAutocompleteDisabled') autocompleteDisabled: boolean
  @Input('autocomplete')autocomplete: _MatAutocompleteBase

  @Input() listsFilter : any []
  @Input() searchingString: string



  constructor(@Self() public ngControl: NgControl ) {
    this.ngControl.valueAccessor = this;


   }
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  setDisabledState?(isDisabled: boolean): void {

  }

  get control(){
    return this.ngControl.control as FormControl
 }




}
