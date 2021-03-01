import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable , ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import {map} from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);

  constructor(private http : HttpClient) {}

  getMembers()  {
   return this.http.get<Member[]>(this.baseUrl+ 'users');
  }

  getMember(Id: number)  {
   return this.http.get<Member>(this.baseUrl+ 'users/' + Id);
  }

  registor(model: any) {

    return this.http.post<Member>(this.baseUrl + 'account/register', model).pipe(
      map((response: Member) => {console.log('mapMember', response)}));

    /* .pipe(
      map(( user: User) => {
        if (user) {
            sessionStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
        }
      })

    ); */

  }

  login(model: any) {

    return  this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user); // store user token in current user source
                   }
      })

    );
  }

 setCurrentUser(user: User) {
  this.currentUserSource.next(user);

 }




}


