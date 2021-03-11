import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AuthService } from './_service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BaB ALSaray';
  currentYear: any;
  constructor (private authService : AuthService){}

  ngOnInit(){
    this.currentYear = new Date().getFullYear();
    this.setCurrentUser();

  }

  setCurrentUser() {
   //When loading take info from local storage and send to auth service
    const token = localStorage.getItem('token');
    if (token) {
    const user: User =  JSON.parse(atob(token.split('.')[1]));
    this.authService.setCurrentUser(user);
    }

  }

}


