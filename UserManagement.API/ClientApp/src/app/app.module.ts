import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListUsersComponent } from './usermanagement/list-users/list-users.component';
import { ManageUserComponent } from './usermanagement/manage-user/manage-user.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material-module';

@NgModule({
  declarations: [
    AppComponent,
    ListUsersComponent,
    ManageUserComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    FormsModule,   
    ReactiveFormsModule
  ],
  providers: [],
  entryComponents:[ManageUserComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
