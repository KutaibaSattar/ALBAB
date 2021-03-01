import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './_account/dashboard/dashboard.component';
import { LoginComponent } from './_account/login/login.component';
import { MemberDetailsComponent } from './_account/member-details/member-details.component';
import { MemberListComponent } from './_account/member-list/member-list.component';

const routes: Routes = [
  {path: '', component: DashboardComponent },
  {path: 'members', component: MemberListComponent },
  {path: 'memberdetails', component: MemberDetailsComponent },
  {path: 'dashboard', component: DashboardComponent },
  {path: 'login', component: LoginComponent },
  {path: '**' ,component: DashboardComponent, pathMatch:'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
