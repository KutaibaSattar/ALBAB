import { Component, OnInit } from '@angular/core';
import { dbAccountStatement } from 'app/models/reports';
import { ReportsService } from 'app/services/reports.service';

@Component({
  selector: 'app-dbaccount-report',
  templateUrl: './dbaccount-report.component.html',
  styleUrls: ['./dbaccount-report.component.scss']
})
export class DbaccountReportComponent implements OnInit {

  constructor(private reportsService : ReportsService) {  }

  accountStatement : dbAccountStatement[];

  ngOnInit(): void {

    this.reportsService.getAccountStatement().subscribe(
      res =>this.accountStatement = res

    )

  }

}
