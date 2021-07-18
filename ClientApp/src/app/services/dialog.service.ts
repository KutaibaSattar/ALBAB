import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { AddressEditingComponent } from 'app/account/profile/address-editing/address-editing.component';
import { MemberAddress } from 'app/models/memberaddress';


@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor( private matDialog : MatDialog) { }

  openDataChangerDialog(memberAddress? : MemberAddress)
  : MatDialogRef<AddressEditingComponent>
  {
    let dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass="dialog-box";
    dialogConfig.autoFocus = true;
    dialogConfig.width="500px"
    dialogConfig.data = memberAddress
    return this.matDialog.open(AddressEditingComponent,dialogConfig)


  }
}
