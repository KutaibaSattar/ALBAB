import { Component, Input, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';



@Component({
  selector: 'app-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.scss']
})
export class AutocompleteComponent  {

  @ViewChild(MatAutocomplete) autocomplete: MatAutocomplete;
  filtered$: Observable<Array<any>>;
  @Input() controlName : FormControl
  @Input() listsFilter : any []
  


  constructor() { }

  ngOnInit(): void {

    this.attachedFilter();

  }

  attachedFilter(): any {
    this.filtered$ = this.controlName.valueChanges.pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))
    );
  }

  filter(val: any): any {
    if (this.listsFilter !== undefined) {
      return this.listsFilter.filter((item:any) => {
        // If the user selects an option, the value becomes a Human object,
        // therefore we need to reset the val for the filter because an
        // object cannot be used in this toLowerCase filter
        let x = typeof val;
        if (typeof val === 'string') {
          const TempString = item.name + ' - ' + item.keyId;
          return TempString.toLowerCase().includes(val.toLowerCase());
        }
      });
    }
  }

  displayFn(item: number): string {
    if (item) {
      return this.listsFilter.find((element) => element.id === item).name;

    }
  }

}
