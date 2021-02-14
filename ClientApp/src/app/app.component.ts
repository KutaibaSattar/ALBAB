import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  constructor (private http : HttpClient){}

  ngOnInit(){
    this.http.get('https://localhost:5001/api/dbaccounts').subscribe((res:Response) =>{

    console.log(res)

    })

  }

}


