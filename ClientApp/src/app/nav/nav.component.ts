import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor(public authService : AuthService) { }
  currentUser$ : Observable<User>;

  ngOnInit(): void {
    this.currentUser$ = this.authService.currentUser$;

  }

  logOut(){
    this.authService.logOut();
  }

}
