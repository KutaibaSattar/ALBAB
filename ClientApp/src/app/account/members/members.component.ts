import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Member } from '../../models/member';
import { AuthService } from '../../services/auth.service';
import { NewMemberComponent } from '../newmember/newmember.component';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit {
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
        this.memberService.getMembers().subscribe((res) =>
        {
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
      this.dialog.open(NewMemberComponent, dialogConfig)

      .afterClosed().subscribe(res => console.log('close',res));


    }

  }



