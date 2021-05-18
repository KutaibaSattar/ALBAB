import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-dropown-template',
  templateUrl: './dropown-template.component.html',
  styleUrls: ['./dropown-template.component.scss']
})
export class DropownTemplateComponent implements OnInit {

  filtered$: Observable<Array<any>>;
  @Input() controlName : FormControl
  @Input() listsFilter : any[]
  @Input() searchingString: string

  constructor() { }

  ngOnInit(): void {

    this.attachedUserFilter();

  }

  attachedUserFilter(): any {
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
          const TempString = item.name //+ ' - ' + item.userId;
          return TempString.toLowerCase().includes(val.toLowerCase());
        }
      });
    }
  }

  displayFn(item: number): string {
    if (item) {
      let x = this.listsFilter.find((element) => element.id === item).name;
      return x;
    }
  }

}
