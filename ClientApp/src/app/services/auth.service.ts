import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable , ReplaySubject } from 'rxjs';
import { environment } from 'environments/environment';
import { Member } from '../models/member';
import {map} from 'rxjs/operators';
import { User } from '../models/user';
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
  constructor(private http: HttpClient, private httpBackend: HttpBackend) {}


  /*how many previous values do we want it to store?
  Now, we're only interested in one value here, so we're just going to add the value of one.*/
  private currentUserSource = new ReplaySubject<User>(1); // buffer for only one user object

   currentUser$ = this.currentUserSource.asObservable(); // $ at end as convention that is Observable

  getMembers()  {
   return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(Id: number)  {
   return this.http.get<Member>(this.baseUrl + 'users/' + Id);
  }

  registor(model: any) {

    return this.http.post<Member>(this.baseUrl + 'account/register', model).pipe(
      map((response: Member) => {console.log('mapMember', response); }));

    /* .pipe(
      map(( user: User) => {
        if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
        }
      })

    ); */

  }

  login(credential: any): Observable<boolean> {

    // return the observable of response but we need true or faulse
    // let httpWithoutIntercep = new HttpClient(this.httpBackend)
    const httpWithoutIntercep = this.http;

    return httpWithoutIntercep.post(this.baseUrl + 'account/login', credential).pipe(
      map((response: User) => {
       const user = response;

       if (user){
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user); // store user in current user source
          return true;
       }
       return false;


      })
    );

  }

setCurrentUser(user: User){
  this.currentUserSource.next(user);  // store user in current user source
  this.LoggedIn();

}

  getCurrentUser(user: User) {
  const token = localStorage.getItem('token');
  if (!token) { return null; }

  const jwtHelper =  new JwtHelperService('token');

  // this.currentUserSource.next(user);

 }

 logOut(){
  localStorage.removeItem('token');
  this.currentUserSource.next(null);


 }

 LoggedIn(){
  const token = localStorage.getItem('token');
  if (!token) { return false; }
  const jwtHelper = new JwtHelperService();
  const expDate = jwtHelper.getTokenExpirationDate(token);
  if (jwtHelper.isTokenExpired(token))
  {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }

}

getDecodedToken(token){
  const x = JSON.parse(atob(token.split('.')[1]));
  console.log('atob', x);
  console.log(Date.now(), x.exp * 1000);
  return JSON.parse(atob(token.split('.')[1]));
}




}


