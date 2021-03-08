import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_service/auth.service';

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

  signIn (credential : any) {


    this.authService.login(credential).subscribe((result : any) => {

      if (result)
      this.router.navigate(['/']);
      else
      this.invalidLogin = true;

      });

    }

     /*  }, error => { console.log(error);
      });

  } */

}
