import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './account/dashboard/dashboard.component';
import { LoginComponent } from './account/login/login.component';
import { MemberDetailsComponent } from './account/member-details/member-details.component';
import { MemberListComponent } from './account/member-list/member-list.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { BrowserModule } from '@angular/platform-browser';
import { ProductsComponent } from './products/products.component';
import { HomeComponent } from './home/home.component';
import { PurchasesComponent } from './purchases/purchases.component';




const routes: Routes = [
  {path: '', component: DashboardComponent },
  {path: 'members', component: MemberListComponent },
  {path: 'memberdetails', component: MemberDetailsComponent },
  {path: 'dashboard', component: HomeComponent },
  {path: 'login', component: LoginComponent },
  {path: 'product', component: ProductsComponent },
  {path: 'purchases', component: PurchasesComponent },
    {path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent, data: { breadcrumb: 'Not Found' } },
  { path: 'server-error', component: ServerErrorComponent, },
  {path: '**' , component: DashboardComponent, pathMatch: 'full' },


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
