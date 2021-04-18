import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from 'app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class Authguard implements CanActivate {

  constructor(private router:Router , private authService: AuthService, private toastr: ToastrService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<boolean>{

    return this.authService.currentUser$.pipe(

      map(user =>{
        if (user) return true;
        this.router.navigate(['/login'],{queryParams :{returnUrl: state.url}} );
        this.toastr.error('Not authorized')
        return false;
      })

    )



  }
}
