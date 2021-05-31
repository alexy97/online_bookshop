import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Book } from './Model/Book';
@Injectable({
  providedIn: 'root'
})
export class DataServiceService {

  baseURL: string = "https://localhost:44397/api/books";
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }

  public getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.baseURL).pipe(
      catchError(this.handleError<Book[]>('getBooks', []))
    );;
  }

  public getBook(id: number): Observable<Book> {
    const url = this.baseURL + "/" + id;
    return this.http.get<Book>(url).pipe(
      catchError(this.handleError<Book>('getBook'))
    );;
  }

  public updateBook(book: Book): Observable<any> {
    const url = this.baseURL + "/" + book.id;
    return this.http.put(url, book, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateBook'))
    );
  }

  public addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.baseURL, book, this.httpOptions).pipe(
      catchError(this.handleError<Book>('addBook'))
    );
  }

  public deleteBook(id: number): Observable<Book> {
    const url = this.baseURL + "/" + id;
    return this.http.delete<Book>(url, this.httpOptions).pipe(
      catchError(this.handleError<Book>('deleteBook'))
    );
  }

  public searchBook(term: string): Observable<Book[]> {
    if (!term.trim()) {
      return of([]);
    }
    var url = this.baseURL + "/?title=" + term;
    return this.http.get<Book[]>(url).pipe(
      catchError(this.handleError<Book[]>('searchBooks', []))
    );
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      console.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
}
