import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Journal, JournalEntry, JournalSingle } from 'app/models/journal';
import { JournalService } from 'app/services/journal.service';
import { observeOn } from 'rxjs/operators';

@Component({
  selector: 'app-journalentry',
  templateUrl: './journalentry.component.html',
  styleUrls: ['./journalentry.component.scss'],
})
export class JournalentryComponent implements OnInit {
  constructor(private JournalService: JournalService) {}

  frmJournal = new FormGroup({
    jeNo: new FormControl(''),
    note: new FormControl(''),
    entryDate: new FormControl(''),
    journalAccounts : new FormArray([this.initSection()]),
  });

  journalNo  = this.frmJournal.get('jeNo');
  note  = this.frmJournal.get('note');
  entryDate  = this.frmJournal.get('entryDate');
  journalAccounts = this.frmJournal.get('journalAccounts') as FormArray


  frmSingleAccount =  new FormGroup ({
    account: new FormControl(),
    note: new FormControl(''),
    dueDate: new FormControl(''),
    credit: new FormControl(null),
    debit: new FormControl(null),
  });

  ngOnInit(): void {
    this.JournalService.getJournal().subscribe((res) => {
      console.log(res);
    });


  }

  initSection(): FormGroup {
    return new FormGroup({
      account: new FormControl(),
      note: new FormControl(''),
      dueDate: new FormControl(''),
      credit: new FormControl(null),
      debit: new FormControl(null),
    });
  }

  addRecord() {
      this.journalAccounts.push(this.initSection());
    // Build the account Auto Complete values

  }

  addItem(frm: JournalSingle) {


    //journal.journalAccounts = []
    /*  let combined = [];
    combined = _.map(frm, (parent) => {
      parent.child = frm.accounts.find(child => child.parent.id === parent.id);
  return parent;
}); */

    //var result = Object.assign(frm.journalAccounts,frm.singleAccount)
    //journal.journalAccounts.push(journal.singleAccount)
    //delete journal.singleAccount
    //console.log(journal)
    //console.log ('second',journal)

   let journal : Journal = JSON.parse(JSON.stringify(this.frmJournal.value))


   /*  let journal : Journal = {...this.frmJournal.value}; */

   journal.journalAccounts.push(this.frmSingleAccount.value)
    console.log(journal)
    console.log(this.frmJournal)


    /* var joinForm = new FormGroup({form1:this.frmJournal});
    console.log(joinForm);


    (<FormArray>joinForm.get('journalAccounts')).push(this.frmSingleAccount)

    console.log(joinForm); */
  }
}
