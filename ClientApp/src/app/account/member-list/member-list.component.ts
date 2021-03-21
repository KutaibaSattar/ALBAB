import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Member } from '../../_models/member';
import { AuthService } from '../../_services/auth.service';
import { MemberDetailsComponent } from '../member-details/member-details.component';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  memberList : Member[];
  constructor(private memberService : AuthService,private dialog : MatDialog ) { }

  form: FormGroup = new FormGroup({
    $key: new FormControl(null),
    userId: new FormControl(''),
    dispalyName: new FormControl(''),
  });

  ngOnInit(): void {

    this.loadMembers()

      }

  loadMembers(){
        this.memberService.getMembers().subscribe((res)=>{
          this.memberList = res;
          console.log(this.memberList);
      });

    }

    AddOrUpdateUser(){
      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      dialogConfig.disableClose = true;
      dialogConfig.width = "50%"
      dialogConfig.data = {};
      this.dialog.open(MemberDetailsComponent, dialogConfig)

      .afterClosed().subscribe(res => console.log('close',res));


    }

  }



