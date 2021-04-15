import {Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Member } from 'app/models/member';
import { AuthService } from 'app/services/auth.service';



@Component({
  selector: 'app-newmember',
  templateUrl: './newmember.component.html',
  styleUrls: ['./newmember.component.scss']
})
export class NewMemberComponent implements OnInit {
  formData: Member;

  constructor(public dialogRef: MatDialogRef<NewMemberComponent>,
              private accountService: AuthService  ) { }


  ngOnInit(): void {
     /* this.formData = {
      userName : '',
      phoneNumber : ''
    } */
  }

  // tslint:disable-next-line: typedef
  onSubmit(form: NgForm){
    // tslint:disable-next-line: deprecation
    this.accountService.registor(form.value).subscribe(
      response => {console.log('response' , response); });
      // console.log(form.value);
    this.dialogRef.close();

  }


}
