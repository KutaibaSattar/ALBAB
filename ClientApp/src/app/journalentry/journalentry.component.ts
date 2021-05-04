import { Component, OnInit } from '@angular/core';
import { Journal } from 'app/models/journal';
import { JournalService } from 'app/services/journal.service';

@Component({
  selector: 'app-journalentry',
  templateUrl: './journalentry.component.html',
  styleUrls: ['./journalentry.component.scss']
})
export class JournalentryComponent implements OnInit {

  constructor(private JournalService : JournalService) { }

  ngOnInit(): void {

 this.JournalService.getJournal().subscribe(
      (res) => {
        console.log(res);
      }
    );



  }

}
