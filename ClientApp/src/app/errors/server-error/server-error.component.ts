import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
  error: any;
  constructor(private router : Router ) {
     // Router state can only accessed from constructor not fro ngOnInit
    
     /*  double question marks used as a shorthand syntax for asking if an expression
      evaluates to null and if it does, we supply a value that is given instead of null.
       Not unlike these two cases, the question mark in the case of null propagation 
       indicates that we will not proceed to what is on the right hand side if what is on the left is null. */
    
       const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.error;
    ;
    
  }

  ngOnInit(): void {
  }

}
