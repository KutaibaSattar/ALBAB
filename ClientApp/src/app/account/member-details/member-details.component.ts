import {Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Member } from 'app/_models/member';
import { AuthService } from 'app/_services/auth.service';



@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss']
})
export class MemberDetailsComponent implements OnInit {
  formData: Member;

  constructor(public dialogRef: MatDialogRef<MemberDetailsComponent>,
   private accountService :AuthService  ) { }


  ngOnInit(): void {
     /* this.formData = {
      userName : '',
      phoneNumber : ''
    } */
  }

  onSubmit(form:NgForm){
    this.accountService.registor(form.value).subscribe(
      response => {console.log('response' ,response)});
      //console.log(form.value);
      this.dialogRef.close();

  }


}
