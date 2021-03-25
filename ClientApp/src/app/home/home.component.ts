import { Component, Injectable, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class Service {
  constructor(private httpClient: HttpClient) { }

  jokes = [];

  getData() {
    return this.jokes.length ? of(this.jokes)
      : this.httpClient.get<any>('https://localhost:5001/api/users').pipe(
        map((data) => {
          this.jokes = data;
          console.log(this.jokes);
          return this.jokes;

        })
      )
  }
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  jokes;

  currentJoke = '';
  constructor(private service: Service) { }

  ngOnInit(): void {
  }

  doFilter() {
    this.jokes = this.service.getData()
      .pipe(map(jokes => this.filter(jokes)),
      )
  }

  filter(values) {
    console.log(values)
    return values.filter(joke => joke.displayName.toLowerCase().includes(this.currentJoke))
  }
displayFn(user: any): any {
    console.log(user);

    // return user && user.userId ? user.displayName + ' - ' + user.userId : '';
    if (user){
   let name =  this.jokes.find(x => x.userId === user).displayName
   return name;
  }

    // return user ? this.jokes.find(x => x.userId === user).displayName : undefined;

     // return 'Hello';
   }
}
