import { Component, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { AccountService } from '../../_service/account.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  memberList : Member[];
  constructor(private memberService : AccountService ) { }

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
      console.log('Hello');

    }

  }

 

