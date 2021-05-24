import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Member } from 'app/models/member';
import { AuthService } from 'app/services/auth.service';
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


  constructor(public authService : AuthService) { }

  ngOnInit(): void {

    this.attachedFilter();
    this.filterDirectly('Abd');

  }

  attachedFilter(): any {
    this.filtered$ = this.controlName.valueChanges.pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filterDirectly(val))
    );

    this.filtered$ = this.controlName.valueChanges.pipe(
      startWith(''),
      /*map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.users.slice()),*/
      map((val) => this.filter(val))
    );
  }

  filterDirectly(val) {
   this.authService.getMembers().pipe(
    map((member) => {
      console.log('New Member', member);
        let rtn =  member.filter(item =>item.name.includes(val));
        return rtn;
      }))


    /* .subscribe((data) => {
      console.log('after Sub',data)

    }



    ) */
  }



  filter(val: any) {
    if (this.listsFilter !== undefined) {
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
    if (item) {
      let IdName = this.listsFilter.find((element) => element.id === item).name;
      console.log('Name',IdName)
      return IdName;
    }
  }

}
