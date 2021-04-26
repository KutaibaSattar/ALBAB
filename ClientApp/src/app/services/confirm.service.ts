import { Injectable } from '@angular/core';
import { ConfirmDialogComponent } from 'app/guards/confirm-dialog/confirm-dialog.component';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal'
import { Observable, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  bsModelRef : BsModalRef;

  constructor(private modalService : BsModalService) { }

  confirm(title='Confirmation',
          message='Are you sure you want to do this ?',
          btnOkTest = 'ok',
          btnCancelTest ='Cancel') : Observable<boolean>{

         const config = {
           initialState: {
             title,
             message,
             btnOkTest,
             btnCancelTest,
           },
         };

         this.bsModelRef = this.modalService.show(ConfirmDialogComponent, config)

         return new Observable<boolean>(this.getResult());
  }
  private getResult(){
    return (observer)=>{

      const subscription = this.bsModelRef.onHidden.subscribe( () =>{
        observer.next(this.bsModelRef.content.result);
        observer.complete();

      });
      return{
        unsubscribe() {
          subscription.unsubscribe()

        }

      }

    }

  }
}
