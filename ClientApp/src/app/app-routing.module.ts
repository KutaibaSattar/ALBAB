import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './_account/dashboard/dashboard.component';
import { LoginComponent } from './_account/login/login.component';
import { MemberDetailsComponent } from './_account/member-details/member-details.component';
import { MemberListComponent } from './_account/member-list/member-list.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { BrowserModule } from '@angular/platform-browser';
import { ProductsComponent } from './products/products.component';





const routes: Routes = [
  {path: '', component: DashboardComponent },
  {path: 'members', component: MemberListComponent },
  {path: 'memberdetails', component: MemberDetailsComponent },
  {path: 'dashboard', component: DashboardComponent },
  {path: 'login', component: LoginComponent },
  {path: 'product', component: ProductsComponent },
  {path: 'errors', component: TestErrorsComponent },
  { path: 'not-found', component: NotFoundComponent, data: { breadcrumb: 'Not Found' } },
  { path: 'server-error', component: ServerErrorComponent, },
  {path: '**' ,component: DashboardComponent, pathMatch:'full' },


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
