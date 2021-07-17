import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-address-editing',
  templateUrl: './address-editing.component.html',
  styleUrls: ['./address-editing.component.scss']
})
export class AddressEditingComponent implements OnInit {

  formGroup: FormGroup;
  constructor() {

    this.formGroup = new FormGroup({
      line1: new FormControl(null),
      line2: new FormControl(null),
      region: new FormControl(null),
      city: new FormControl(null),
      country: new FormControl(null),
    })


   }



  ngOnInit(): void {
  }

}
