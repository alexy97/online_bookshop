import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Book } from '../Model/Book';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  public selectedBook?: Book;
  public active = false;
  public thumbnailSrc =
    "https://i.ibb.co/JyZcLcR/unnamed.png";
  public cover = "https://s26162.pcdn.co/wp-content/uploads/2018/12/11-bookstores-6-three-lives-2.w710.h473.2x.jpg";

  @Input() public set model(book: Book | undefined) {
    this.selectedBook = book;
    this.active = book !== undefined;
  }
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Book> = new EventEmitter();
  
 
  constructor() { }

  ngOnInit(): void {
  }
  resetSelection() {
    this.selectedBook = undefined;
  }

  public Buy(title: string, price: number) {
    alert("Successfully purchased " + title + " for $" + price + "!");
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  public closeForm(): void {
    this.active = false;
    this.cancel.emit();
  }
}
