import {Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Member } from 'src/app/_models/member';



@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss']
})
export class MemberDetailsComponent implements OnInit {
  formData: Member

  constructor() {}


  ngOnInit(): void {

    /* this.formData = {
      userName : '',
      phoneNumber : ''
    } */
  }


}
