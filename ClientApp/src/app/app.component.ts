import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  currentYear: any;
  constructor (){}

  ngOnInit(){
    this.currentYear = new Date().getFullYear();
    console.log(this.currentYear);
  
  }

}


