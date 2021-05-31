import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksListComponent } from './books-list/books-list.component';
import { HomeComponent } from './home/home.component';
import { InventoryManagementComponent } from './inventory-management/inventory-management.component';
import { APP_BASE_HREF } from "@angular/common";
import { AuthGuard } from "./auth/auth.guard";

const drawerRoutes = [
  {
    path: "",
    component: HomeComponent,
    text: "Home",
    icon: "k-i-home",
  },
  {
    path: "books",
    component: BooksListComponent,
    text: "Books Catalog",
    icon: "k-i-list-numbered",
  },
  {
    path: "inventory",
    component: InventoryManagementComponent,
    text: "Inventory Management",
    icon: "k-i-edit",
    canActivate: [AuthGuard]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(drawerRoutes)],
  exports: [RouterModule],
  providers: [{
    provide: APP_BASE_HREF,
    useValue: window.location.pathname,
  },],
})
export class AppRoutingModule {

}
