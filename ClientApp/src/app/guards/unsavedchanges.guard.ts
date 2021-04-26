import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { PurchasesComponent } from 'app/purchases/purchases.component';
import { ConfirmService } from 'app/services/confirm.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UnsavedchangesGuard implements CanDeactivate<unknown> {

  constructor (private confirmService: ConfirmService){}
  canDeactivate(
    component: PurchasesComponent):Observable<boolean> | boolean  {
      if (component.formPurchHdr.dirty) {
       return this.confirmService.confirm();

      }
      return true;

  }

}
