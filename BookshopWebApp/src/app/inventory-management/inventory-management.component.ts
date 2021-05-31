import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { DataBindingDirective } from "@progress/kendo-angular-grid";
import { DataServiceService } from '../data-service.service';
import { Book } from '../Model/Book';
@Component({
  selector: 'app-inventory-management',
  templateUrl: './inventory-management.component.html',
  styleUrls: ['./inventory-management.component.css']
})
export class InventoryManagementComponent implements OnInit {

  public books: Book[] = [];
  public editDataItem?: Book;
  public isNew?: boolean;
  public loading: boolean = true;

  constructor(private bookService: DataServiceService) {

  }

  ngOnInit(): void {
    this.getBooksData();
  }

  getBooksData(): void {
    this.bookService.getBooks().subscribe(books => {
      this.books = books;
      this.loading = false;
    });
  }

  public addHandler() {
    this.editDataItem = new Book();
    this.isNew = true;
  }

  public editHandler({ sender, rowIndex, dataItem }: any) {
    this.editDataItem = dataItem;
    this.isNew = false;
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
  }

  public saveHandler(book: any) {
    var bookToSave = book as Book;

    if (this.isNew) {

      this.bookService.addBook(book).subscribe(book => {
        this.getBooksData();
      });
    }
    else {
      this.bookService.updateBook(book).subscribe(book => {
        this.getBooksData();
      });
    }
    this.editDataItem = undefined;
  }

  public removeHandler({ dataItem }: any) {
    this.bookService.deleteBook(dataItem.id).subscribe(b => {
      this.getBooksData();
    });
  }

}
