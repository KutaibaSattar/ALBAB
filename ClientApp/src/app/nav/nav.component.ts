import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  currentYear: any;
  
  constructor() { }

  ngOnInit(): void {
    this.currentYear = new Date().getFullYear();
    console.log(this.currentYear);
  }

}
