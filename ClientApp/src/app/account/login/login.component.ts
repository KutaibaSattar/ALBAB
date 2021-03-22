import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'app/services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) {}

  returnUrl: string;
  invalidLogin: boolean;
  ngOnInit(): void {
  }

  // tslint:disable-next-line: typedef
  signIn(credential: any) {


    // tslint:disable-next-line: deprecation
    this.authService.login(credential).subscribe((result: boolean) => {

      if (result) {
      this.router.navigate(['/']);
      }
      /* else
      this.invalidLogin = true;
      this.authService.LoggedIn(); */
      });

    }

     /*  }, error => { console.log(error);
      });

  } */

}
