import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { PurchasesComponent } from 'app/purchases/purchases.component';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnsavedchangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: PurchasesComponent):boolean  {
      if (component.purchHdr.dirty) {
        return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')

      }
      return true;

  }

}
