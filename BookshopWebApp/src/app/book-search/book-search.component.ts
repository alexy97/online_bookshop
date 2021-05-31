import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DataServiceService } from '../data-service.service';
import { Book } from '../Model/Book';
import { Observable, Subject } from 'rxjs';
import {
  debounceTime, distinctUntilChanged, switchMap
} from 'rxjs/operators';

@Component({
  selector: 'app-book-search',
  templateUrl: './book-search.component.html',
  styleUrls: ['./book-search.component.css']
})
export class BookSearchComponent implements OnInit {

  private searchTerms = new Subject<string>();
  public selected?: Book;
  books$!: Observable<Book[]>;
  @Output() bookSelected: EventEmitter<Book> = new EventEmitter();

  constructor(private bookService: DataServiceService) { }

  ngOnInit(): void {
    this.books$ = this.searchTerms.pipe(
      // wait 400ms after each keystroke before considering the term
      debounceTime(400),
      distinctUntilChanged(),
      switchMap((term: string) => this.bookService.searchBook(term)),
    );
  }
  search(term: string): void {
    this.searchTerms.next(term);
    this.selected = undefined;
  }

  selectBook(book: Book) {
    this.bookSelected.emit(book);
    this.selected = book;
  }

}
