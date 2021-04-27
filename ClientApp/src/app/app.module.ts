import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MembersComponent } from './account/members/members.component';
import { NewMemberComponent } from './account/newmember/newmember.component';
import { DashboardComponent } from './account/dashboard/dashboard.component';
import {BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatDialogModule} from '@angular/material/dialog';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './account/login/login.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { ProductsComponent } from './products/products.component';
import { invoiceitemComponent } from './invoiceitem/invoiceitem.component';
import { PurchasesComponent } from './purchases/purchases.component';
import { ProfileComponent } from './account/profile/profile.component';
import { DatePipe } from '@angular/common';
import { ConfirmDialogComponent } from './guards/confirm-dialog/confirm-dialog.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker'





@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MembersComponent,
    NewMemberComponent,
    DashboardComponent,
    HomeComponent,
    LoginComponent,
    ProductsComponent,
    invoiceitemComponent,
   PurchasesComponent,
   ProfileComponent,
   ConfirmDialogComponent,
    ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'

    }),
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),




  ],
  providers: [
     // multi=true, in order to allow the angular to create multiple objects for the JwtInterceptorService.
     {provide : HTTP_INTERCEPTORS , useClass : ErrorInterceptor , multi: true} ,
     {provide : HTTP_INTERCEPTORS , useClass : JwtInterceptor , multi: true},
     DatePipe,


  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
