import { Component, OnInit } from '@angular/core';
import { DataServiceService } from '../data-service.service';
import { RowClassArgs } from "@progress/kendo-angular-grid";
import { Book } from '../Model/Book';
import { load } from '@progress/kendo-angular-intl';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit {

  public books: Book[] = [];
  public limit: number = 0;
  public selectedBook?: Book;
  public loading: boolean = true;

  constructor(private bookService: DataServiceService) { }

  ngOnInit(): void {
    this.getBooksData();
  }

  getBooksData(): void {
    this.bookService.getBooks().subscribe(books => {
      this.books = books;
      this.loading = false;
    });
  }

  public Buy(title: string, price: number) {
    alert("Successfully purchased " + title + " for $" + price + "!");
  }
  onSelect(book: Book) {
    this.selectedBook = book;
  }
  bookSelectedHandler(book: Book) {
    this.selectedBook = book;
  }

  public rowCallback = (context: RowClassArgs) => {

    const limitComparison = context.dataItem.quantity < this.limit;
    if (limitComparison)
      return { belowLimit: true }
    else
      return { aboveLimit: true }
  };
}

