import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Member } from '../../_models/member';
import { AccountService } from '../../_service/account.service';
import { MemberDetailsComponent } from '../member-details/member-details.component';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  memberList : Member[];
  constructor(private memberService : AccountService,private dialog : MatDialog ) { }

  form: FormGroup = new FormGroup({
    $key: new FormControl(null),
    userName: new FormControl('')
  });

  ngOnInit(): void {

    this.loadMembers()

      }

  loadMembers(){
        this.memberService.getMembers().subscribe( (res)=>{
          this.memberList = res;
          console.log(this.memberList);
      });

    }

    OnClick(){

      this.dialog.open(MemberDetailsComponent, {data:this.form.controls})

      .afterClosed().subscribe(res => console.log('close',res));


    }

  }



