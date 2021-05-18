import { Component, Injectable, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class Service {
  constructor(private httpClient: HttpClient) { }


  getData() {
    return  this.httpClient.get<Observable<any>>('https://localhost:5001/api/users')
  }
}
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  jokes : Observable<any>;

  currentJoke=1 ;
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
    return values.filter(joke => joke.name.toLowerCase().includes(this.currentJoke))
  }
displayFn(user: any): any {
    console.log(user);

   }
}
