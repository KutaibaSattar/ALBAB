import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Journal } from 'app/models/journal';
import { JournalService } from 'app/services/journal.service';

@Component({
  selector: 'app-journalentry',
  templateUrl: './journalentry.component.html',
  styleUrls: ['./journalentry.component.scss']
})
export class JournalentryComponent implements OnInit {

  constructor(private JournalService : JournalService,private fb: FormBuilder) { }

  frmJournal = this.fb.group({
  journalNo : [null],
  note : new FormControl(),
  entryDate : new FormControl(),

  frmRelatedAccount : this.fb.group({
    relatedAccount : new FormControl(),
    note : new FormControl(),
    amount : new FormControl(),
  }),

  frmAccounts : this.fb.array([
    account : new FormGroup({


    })

  ])

  })

  ngOnInit(): void {

 this.JournalService.getJournal().subscribe(
      (res) => {
        console.log(res);
      }
    );



  }

}
