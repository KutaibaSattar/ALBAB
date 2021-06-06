import {Component, Inject, OnInit, Pipe, PipeTransform } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Member, MemberType } from 'app/models/member';
import { AuthService } from 'app/services/auth.service';



@Component({
  selector: 'app-newmember',
  templateUrl: './newmember.component.html',
  styleUrls: ['./newmember.component.scss']
})
export class NewMemberComponent implements OnInit {
  formData: Member;
  type = 'Client';
  keyId;

 
  memberType = MemberType


  constructor(public dialogRef: MatDialogRef<NewMemberComponent>,
              private accountService: AuthService  ) { }


  ngOnInit(): void {
     /* this.formData = {
      userName : '',
      phoneNumber : ''
    } */
    console.log('Member',this.memberType)
    console.log('type',this.type)

  }

  // tslint:disable-next-line: typedef
  onSubmit(form: NgForm){
    // tslint:disable-next-line: deprecation

    console.log(form.value)
    delete form.value.confirmPassword


    this.accountService.registor(form.value).subscribe(
      response => {console.log('response' , response); });
      // console.log(form.value);
    this.dialogRef.close();

  }




}
