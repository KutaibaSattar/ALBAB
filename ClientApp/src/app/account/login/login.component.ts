import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'app/services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router,private route: ActivatedRoute) {}

  returnUrl: string;
  invalidLogin: boolean;
  ngOnInit(): void {
  }

  // tslint:disable-next-line: typedef
  signIn(credential: any) {


    // tslint:disable-next-line: deprecation
    this.authService.login(credential).subscribe((result: boolean) => {

      if (result) {

      let returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
      this.router.navigate([returnUrl || '/']);
      }
         },(result) => {
          console.log(result);
          this.invalidLogin=true;

         }
     );

    }



}
