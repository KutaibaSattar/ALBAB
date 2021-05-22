import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Journal, JournalEntry, JournalSingle } from 'app/models/journal';
import { JournalService } from 'app/services/journal.service';

@Component({
  selector: 'app-journalentry',
  templateUrl: './journalentry.component.html',
  styleUrls: ['./journalentry.component.scss'],
})
export class JournalentryComponent implements OnInit {
  constructor(
    private JournalService: JournalService,
    private fb: FormBuilder
  ) {}

  frmJournal = this.fb.group({
    journalNo: new FormControl(''),
    note: new FormControl(''),
    entryDate: new FormControl(''),

    singleAccount: this.fb.group({
      account: new FormControl(),
      note: new FormControl(''),
      dueDate: new FormControl(''),
      credit: new FormControl(null),
      debit: new FormControl(null),
    }),

    journalAccounts: this.fb.array([this.initSection()]),
  });

  journalNo  = this.frmJournal.get('journalNo');
  note  = this.frmJournal.get('note');
  entryDate  = this.frmJournal.get('entryDate');


  ngOnInit(): void {
    this.JournalService.getJournal().subscribe((res) => {
      console.log(res);
    });
  }

  initSection(): FormGroup {
    return this.fb.group({
      account: new FormControl(),
      note: new FormControl(''),
      debit: new FormControl(null),
      credit: new FormControl(null),
      dueDate: new FormControl(''),
    });
  }
  addItem(frm: JournalSingle) {
    console.log('first', frm);
    console.log(this.frmJournal.value);

    let journal = new Journal();

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
    journal.journalNo = this.journalNo.value;
    journal.entryDate = this.entryDate.value;
    journal.note = this.note.value;

    journal.journalAccounts = this.frmJournal.get('journalAccounts').value;
    journal.journalAccounts.push(this.frmJournal.get('singleAccount').value);

    console.log(journal);
  }
}
