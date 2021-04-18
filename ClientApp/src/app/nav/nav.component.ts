import { Component, OnInit } from '@angular/core';
import { Itoken } from 'app/models/token';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {

  constructor(public authService : AuthService) { }


  ngOnInit(): void {
  

  }

  logOut(){
    this.authService.logOut();
  }



}
