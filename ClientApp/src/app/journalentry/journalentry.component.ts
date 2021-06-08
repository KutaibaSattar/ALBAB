import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Journal, JournalEntry, JournalType} from 'app/models/journal';
import { AuthService } from 'app/services/auth.service';
import { DbAccountService } from 'app/services/dbaccount.service';
import { JournalService } from 'app/services/journal.service';
import { ProductService } from 'app/services/product.service';
import { forkJoin } from 'rxjs';
import { observeOn } from 'rxjs/operators';

@Component({
  selector: 'app-journalentry',
  templateUrl: './journalentry.component.html',
  styleUrls: ['./journalentry.component.scss'],
})
export class JournalentryComponent implements OnInit {
  constructor(private JournalService: JournalService,private dbaccountService: DbAccountService,
    private authService : AuthService,private cdr: ChangeDetectorRef  ) {}

  grdTotal = new FormControl(0); // sepearated

  frmJournal = new FormGroup({
    jeNo: new FormControl(''),
    note: new FormControl(''),
    entryDate: new FormControl(''),
    type:new FormControl(''),
    journalAccounts : new FormArray([this.initSection()]),
  });



  frmSingleAccount =  new FormGroup ({
    account: new FormControl(),
    note: new FormControl(''),
    dueDate: new FormControl(''),
    credit: new FormControl(null),
    debit: new FormControl(null),
  });

  JeNo  = this.frmJournal.get('jeNo');
  note  = this.frmJournal.get('note');
  entryDate  = this.frmJournal.get('entryDate') as FormControl;
  journalAccounts = this.frmJournal.get('journalAccounts') as FormArray


  account = this.frmSingleAccount.get('account') as FormControl
  dueDate  = this.frmSingleAccount.get('dueDate') as FormControl;

  accounts : FormControl[] = new Array();
  dueDates : FormControl[] = new Array();

  journalType =  Object.keys(JournalType)



  ngOnInit(): void {
    // this.JournalService.getJournal().subscribe((res) => {
    //   console.log(res);
    // });

    let sources = [
      this.dbaccountService.getFlattenDbAccounts(),
      this.authService.getMembers(),
    ];

    forkJoin(sources).subscribe();


    this.accounts[0] =  this.journalAccounts.at(0).get('accounts') as FormControl
    this.dueDates[0] =  this.journalAccounts.at(0).get('dueDate') as FormControl

      // this.journalAccounts.at(0).get('debit')
      // .valueChanges.subscribe(() => this.updateTotalUnitPrice());

    console.log ('journal',this.journalType);

  }

  initSection(): FormGroup {
    return new FormGroup({
      accounts: new FormControl(),
      note: new FormControl(''),
      dueDate: new FormControl(''),
      credit: new FormControl(null),
      debit: new FormControl(null),
    });


  }

  addRecord() {
      this.journalAccounts.push(this.initSection());
      this.accounts[this.journalAccounts.length-1] =  this.journalAccounts.at(this.journalAccounts.length-1).get('accounts') as FormControl
      this.dueDates[this.journalAccounts.length-1] =  this.journalAccounts.at(this.journalAccounts.length-1).get('dueDate') as FormControl
      this.journalAccounts.at(this.journalAccounts.length-1).get('debit')
      .valueChanges.subscribe(() => this.updateTotalUnitPrice());
    // Build the account Auto Complete values

  }

  private updateTotalUnitPrice() {

    this.grdTotal.setValue(this.journalAccounts.getRawValue()
        .reduce((sum, current) => sum + +current.debit, 0)
    );
  }


    removeUnit(i: number) {
      this.journalAccounts.removeAt(i);
      this.accounts.splice(i,1);
      this.dueDates.splice(i,1);
      this.updateTotalUnitPrice();
    }



  onSubmit() {



   let journal : Journal = JSON.parse(JSON.stringify(this.frmJournal.value))


   /*  let journal : Journal = {...this.frmJournal.value}; */

   this.frmSingleAccount.value.credit = this.grdTotal.value
   journal.journalAccounts.push(this.frmSingleAccount.value)
    console.log(journal)
    console.log(this.frmJournal)
    this.JournalService.updateJournal(journal);


  }

  ngAfterContentChecked() {
    this.cdr.detectChanges();
 // call or add here your code
}
}
