import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor( private route: ActivatedRoute) { }

  userId : string;

  ngOnInit(): void {
   this.userId = this.route.snapshot.paramMap.get('userId');

  }

}
