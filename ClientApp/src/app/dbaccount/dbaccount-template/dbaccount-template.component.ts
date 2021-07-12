import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-dbaccount-template',
  templateUrl: './dbaccount-template.component.html',
  styleUrls: ['./dbaccount-template.component.scss']
})
export class DbaccountTemplateComponent implements OnInit {
  @Input() isChild: boolean
  constructor(private controlContainer: ControlContainer) { }
  public form: FormGroup;






  ngOnInit(): void {



      this.form = <FormGroup>this.controlContainer.control;





    console.log(this.isChild)
  }

}
