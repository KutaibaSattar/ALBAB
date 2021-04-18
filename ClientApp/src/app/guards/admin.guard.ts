import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from 'app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private authService : AuthService, private toastr: ToastrService){}
  canActivate(): Observable<boolean > {
    return this.authService.currentUser$.pipe(
      map(user =>{
        if (user.roles.includes('Admin') || user.roles.includes('Moderator'))
        return true;
        this.toastr.error('You cannot enter this area');
      })

    )
  }

}
