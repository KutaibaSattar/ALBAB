import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MemberListComponent } from './_account/member-list/member-list.component';
import { MemberDetailsComponent } from './_account/member-details/member-details.component';
import { DashboardComponent } from './_account/dashboard/dashboard.component';
import {BrowserAnimationsModule } from '@angular/platform-browser/animations'
import {MatDialogModule} from '@angular/material/dialog';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './_account/login/login.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';






@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MemberListComponent,
    MemberDetailsComponent,
    DashboardComponent,
    HomeComponent,
    LoginComponent,
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDialogModule,



  ],
  providers: [
 /*   {
    //multi=true, in order to allow the angular to create multiple objects for the JwtInterceptorService.
   provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true
   } */
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
