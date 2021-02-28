import {Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Member } from 'src/app/_models/member';



@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss']
})
export class MemberDetailsComponent implements OnInit {
  formData: Member;

  constructor(@Inject(MAT_DIALOG_DATA) public data, public dialogRef: MatDialogRef<MemberDetailsComponent>) {
     console.log("Data", data )}


  ngOnInit(): void {
    this.formData ={
      id:null,
      userId: this.data,
      password: '',
      displayName: '',

      }




    /* this.formData = {
      userName : '',
      phoneNumber : ''
    } */
  }

  onSubmit(form:NgForm){
      console.log(form.value);
      this.dialogRef.close();

  }


}
