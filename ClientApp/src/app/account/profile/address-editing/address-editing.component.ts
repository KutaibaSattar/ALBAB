import { Component, OnInit , Inject} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MemberAddress } from 'app/models/memberaddress';
import { MemberAddressService } from 'app/services/address.service';

@Component({
  selector: 'app-address-editing',
  templateUrl: './address-editing.component.html',
  styleUrls: ['./address-editing.component.scss']
})
export class AddressEditingComponent implements OnInit {

  formGroup: FormGroup;
  constructor(@Inject(MAT_DIALOG_DATA) public dialogData : MemberAddress,
  public matDialogRef: MatDialogRef<AddressEditingComponent>, public addressService : MemberAddressService)
   {

    this.formGroup = new FormGroup({

      line1: new FormControl(null),
      line2: new FormControl(null),
      region: new FormControl(null),
      city: new FormControl(null),
      country: new FormControl(null),
    })
   }


  ngOnInit(): void {
    this.formGroup.patchValue({

      line1 : this.dialogData.line1,
      line2: this.dialogData.line2,
      region: this.dialogData.region,
      city: this.dialogData.city,
      country: this.dialogData.country,
    })
  }

  onCancelClick(){
    this.matDialogRef.close();

  }

  onSaveClick(){



    this.addressService.newAddress({...this.formGroup.value, appUserId: this.dialogData.appUserId}).subscribe(
    response =>  {
        console.log(response)
        this.matDialogRef.close({action:"Saved", data: response})
      }

    )
  }
}
