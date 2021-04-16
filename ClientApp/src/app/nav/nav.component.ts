import { Component, OnInit } from '@angular/core';
import { Itoken } from 'app/models/token';
import { Observable } from 'rxjs';
import { IUser } from '../models/user';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {

  constructor(public authService : AuthService) { }
  currentUser$ : Observable<Itoken>;

  ngOnInit(): void {
   this.currentUser$ = this.authService.currentUser$;

  }

  logOut(){
    this.authService.logOut();
  }



}
