import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './account/dashboard/dashboard.component';
import { LoginComponent } from './account/login/login.component';
import { NewMemberComponent } from './account/newmember/newmember.component';
import { MembersComponent } from './account/members/members.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { BrowserModule } from '@angular/platform-browser';
import { ProductsComponent } from './products/products.component';
import { HomeComponent } from './home/home.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { ProfileComponent } from './account/profile/profile.component';
import { Authguard } from './guards/auth.guard';
import { UnsavedchangesGuard } from './guards/unsavedchanges.guard';
import { JournalentryComponent } from './journalentry/journalentry.component';
import { DbaccountComponent } from './dbaccount/dbaccount.component';
import { TreeChecklistExampleComponent } from './tree-checklist-example/tree-checklist-example.component';
import { DbaccountReportComponent } from './reports/dbaccount-report/dbaccount-report.component';
import { QuotationComponent } from './quotation/quotation.component';




const routes: Routes = [
  { path: '', component: DashboardComponent },
  {
    path:'',
    runGuardsAndResolvers:'always',
    canActivate:[Authguard],
    children:[
      { path: 'members/:memberKeyId', component: ProfileComponent },
      { path: 'members', component: MembersComponent },
      { path: 'dashboard', component: HomeComponent },
      { path: 'product', component: ProductsComponent},
      { path: 'quotation', component: QuotationComponent},
      { path: 'purchases', component: PurchasesComponent, canDeactivate:[UnsavedchangesGuard]},
      { path: 'reports', component: DbaccountReportComponent},
      { path: 'errors', component: TestErrorsComponent },
      { path: 'not-found',component: NotFoundComponent},
      { path: 'server-error', component: ServerErrorComponent },
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'journalentry', component: JournalentryComponent },
  { path: 'dbAccount', component: DbaccountComponent},
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  declarations:[
    ServerErrorComponent

  ],
  imports: [RouterModule.forRoot(routes),
    BrowserModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
