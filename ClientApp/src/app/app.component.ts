import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Itoken } from './models/token';
import { User } from './models/user';
import { AuthService } from './services/auth.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BaB ALSaray';
  currentYear: any;

  constructor (private authService : AuthService, private router: Router){}

  ngOnInit(){
    this.currentYear = new Date().getFullYear();
   this.getCurrentUser();


  }

  getCurrentUser() {
   //When loading take info from local storage and send to auth service

   const token: string = localStorage.getItem('token');

    if (token) {
      let currentUser = new User()
      const userToken : Itoken = JSON.parse(atob(token.split('.')[1]));
      currentUser.given_name = userToken.given_name
      currentUser.token = token
      this.authService.setCurrentUser(currentUser);

    }

  }



}


