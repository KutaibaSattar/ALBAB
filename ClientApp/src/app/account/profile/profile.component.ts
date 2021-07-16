import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Member } from 'app/models/member';
import { MemberAddressService } from 'app/services/address.service';
import { AuthService } from 'app/services/auth.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor( private route: ActivatedRoute,
               private authService :AuthService,
               private addressService : MemberAddressService) { }

  member: any ;

  ngOnInit(): void {

   this.route.paramMap
   .pipe(map(() =>{

    this.member =  window.history.state.member })).subscribe()







  }

}
