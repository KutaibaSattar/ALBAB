import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddressEditingComponent } from 'app/account/profile/address-editing/address-editing.component';


@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor( private matDialog : MatDialog) { }

  openDataChangerDialog()
  {
    let dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass="dialog-box";
    dialogConfig.autoFocus = true;
    dialogConfig.width="500px"
    this.matDialog.open(AddressEditingComponent,dialogConfig)
  }
}
