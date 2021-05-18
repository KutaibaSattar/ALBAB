import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable , ReplaySubject } from 'rxjs';
import { environment } from 'environments/environment';
import { Member } from '../models/member';
import {map} from 'rxjs/operators';
import { User } from '../models/user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Itoken } from 'app/models/token';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


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
  constructor(private http: HttpClient, private httpBackend: HttpBackend,private router: Router, private toastr: ToastrService) {}


  /*how many previous values do we want it to store?
  Now, we're only interested in one value here, so we're just going to add the value of one.*/
  private currentUserSource = new ReplaySubject<User>(1); // buffer for only one user object
  currentUser$ = this.currentUserSource.asObservable(); // $ at end as convention that is Observable

  getMembers(): Observable<Member[]>  {
   return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(Id: number): Observable<Member>  {
   return this.http.get<Member>(this.baseUrl + 'users/getuser/' + Id);
  }

  registor(model: any) {

    return this.http.post<Member>(this.baseUrl + 'account/register', model).pipe(
      map((response: Member) => {console.log('mapMember', response); }));

  }

  login(credential: any): Observable<boolean> {

    // return the observable of response but we need true or faulse
    // let httpWithoutIntercep = new HttpClient(this.httpBackend)
    const httpWithoutIntercep = this.http;

    return this.http.post(this.baseUrl + 'account/login', credential).pipe(
      map((user: User) => {
       if (user && user.token){
          localStorage.setItem('token', user.token);
          user.given_name = user.name
          this.currentUserSource.next(user); // store user in current user source
          return true;
       }
       return false;


      })
    );

  }

setCurrentUser(user: User){
 user.roles = [];
 const roles = this.getDecodeToken(user.token).role
 Array.isArray(roles) ? user.roles = roles : user.roles.push(roles)
 this.currentUserSource.next(user);  // store user in current user source
 this.currentUser$.subscribe(
   res => console.log('subject',res)
 )
  this.tokenExpired();

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
  this.router.navigate(['/login']);


 }

 tokenExpired(){

  const token = localStorage.getItem('token');

  if (!token) {
     return false;
    }

  const jwtHelper = new JwtHelperService();
  const expDate = jwtHelper.getTokenExpirationDate(token);
  let x = jwtHelper.decodeToken(token)

  if (jwtHelper.isTokenExpired(token))
  {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.toastr.error('Session expired')
    return false;

  }

  return true;

}

getDecodeToken (token)  {
  return JSON.parse(atob(token.split('.')[1]));
  }

}
