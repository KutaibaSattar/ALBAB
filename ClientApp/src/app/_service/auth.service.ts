import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable , ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import {map} from 'rxjs/operators';
import { User } from '../_models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;

 /*  In order to prevent the execution of interceptor for the particular request,
   you have to create object of the HttpClient in a different way;
   and inject a new service called HttpBackend, which represents the HttpClient without interceptors.
   private http = HttpClient;
  constructor(private httpBackend : HttpBackend) {}
  this.httpClient = new HttpClient(this.httpBackend)
  So if you make any request now, as it is not having any interceptors it won't invoke any interceptor.
  That means the Authorization token will not be added for this request.
  So finally you will have two choices.
  If you don't want to send the Authorization token by using an interceptor use HttpXhrBackend.
  If you want to send Authorization token by using interceptor, use HttpClient in a regular way.
 */
  constructor(private http : HttpClient, private httpBackend : HttpBackend) {}


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

  login(credential: any) {

    // return the observable of response but we need true or faulse
    let httpWithoutIntercep = new HttpClient(this.httpBackend)

    return httpWithoutIntercep.post(this.baseUrl + 'account/login',credential).pipe(
      map((response: any) => {
       let result = response;
       if (result && result.token){
          sessionStorage.setItem('currentUser', result.token);
         //this.isLoggedIn();
         return true;
       }
       return false;


      })
    )

  }

 getCurrentUser(user: User) {
  let token = sessionStorage.getItem('currentUser')
  if (!token) return null;

  let jwtHelper =  new JwtHelperService('currentUser');

  //this.currentUserSource.next(user);

 }

 logOut(){
  sessionStorage.removeItem('currentUser');


 }

 isLoggedIn(){
  let token = sessionStorage.getItem('currentUser');

  if (!token) return false;

  const jwtHelper = new JwtHelperService();
  let expDate = jwtHelper.getTokenExpirationDate(token);
  let isExp = jwtHelper.isTokenExpired(token);

  console.log(expDate, isExp);

  return !isExp;


 }




}


