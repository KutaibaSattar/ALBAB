import {Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss']
})
export class MemberDetailsComponent implements OnInit {

  constructor() { }
  form: FormGroup = new FormGroup({
    $key: new FormControl(null),
    userName: new FormControl('')


  });


  ngOnInit(): void {
  }

}
