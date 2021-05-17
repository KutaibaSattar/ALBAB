import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dropown-template',
  templateUrl: './dropown-template.component.html',
  styleUrls: ['./dropown-template.component.scss']
})
export class DropownTemplateComponent implements OnInit {

  @Input() filtered$: Observable<Array<any>>;
  @Input() controlName : FormControl

  constructor() { }

  ngOnInit(): void {

  }

}
