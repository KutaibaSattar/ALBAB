import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { Member } from 'app/models/member';
import { MemberAddress } from 'app/models/memberaddress';
import { MemberAddressService } from 'app/services/address.service';
import { AuthService } from 'app/services/auth.service';
import { map } from 'rxjs/operators';
import { DialogService } from 'app/services/dialog.service';



@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor( private route: ActivatedRoute,
               private addressService : MemberAddressService,
               private dialogService : DialogService
               ) { }

  member: Member ;
  memberAddress : MemberAddress[] =[];
  newMemberAddress : MemberAddress

  columnsToDisplay: string[] = ['line1','line2','region','city','country','action']

  ngOnInit(): void {

   this.route.paramMap
   .pipe(map(() =>{

    this.member =  window.history.state.member


    if(this.member)
    this.addressService.getMemberAddressList(this.member.id).subscribe(
      (res) => {
        this.memberAddress = res
        console.log (this.memberAddress);
      }

    )
  })).subscribe()


  }
  addData(){

  this.newMemberAddress = new MemberAddress();
  this.newMemberAddress.appUserId = this.member.id;

  let dialogRef = this.dialogService.openDataChangerDialog(this.newMemberAddress)

  dialogRef.afterClosed().subscribe((result)=>
  {
   if(result.action == 'Saved' && result.data)
   {
      this.memberAddress.push(result);

   }

  

  })



  }

  removeData(){

  }

}
