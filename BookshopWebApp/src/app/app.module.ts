import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InventoryManagementComponent } from './inventory-management/inventory-management.component';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GridModule } from '@progress/kendo-angular-grid';
import { HttpClientModule } from '@angular/common/http';
import { NavigationDrawerComponent } from './navigation-drawer/navigation-drawer.component';
import { LayoutModule } from '@progress/kendo-angular-layout';

import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { BooksListComponent } from './books-list/books-list.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from "@angular/router";
import { APP_BASE_HREF } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { DialogModule, DialogsModule } from "@progress/kendo-angular-dialog";
import { LabelModule } from "@progress/kendo-angular-label";
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookSearchComponent } from './book-search/book-search.component';
import { UploadsModule } from "@progress/kendo-angular-upload";
import { EditBookComponent } from './edit-book/edit-book.component';
import { DateInputsModule } from "@progress/kendo-angular-dateinputs";
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent,
    InventoryManagementComponent,
    NavigationDrawerComponent,
    BooksListComponent,
    HomeComponent,
    BookDetailsComponent,
    BookSearchComponent,
    EditBookComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    InputsModule,
    BrowserAnimationsModule,
    GridModule,
    HttpClientModule,
    LayoutModule,
    ButtonsModule,
    RouterModule,
    DialogsModule,
    LabelModule,
    ReactiveFormsModule,
    DialogModule,
    UploadsModule,
    DateInputsModule,
    FormsModule
  ],
  providers: [{
    provide: APP_BASE_HREF,
    useValue: window.location.pathname,
  },],
  bootstrap: [AppComponent]
})
export class AppModule { }
