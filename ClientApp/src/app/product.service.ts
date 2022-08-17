import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Product } from './product';
import { MessageService } from './message.service';

@Injectable({ providedIn: 'root' })

export class ProductService {

  private productUrl = '/app/product';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService) { }
  
  /** GET FeatureProducts from the server */
  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productUrl)
      .pipe(
        tap(_ => this.log('Fetched All Products')),
        catchError(this.handleError<Product[]>('getFeatureProducts', []))
      );
  }

  /** GET FeatureProduct by id. Return `undefined` when id not found */
  getFeatureProductNo404<Data>(id: number): Observable<Product> {
    const url = `${this.productUrl}/?id=${id}`;
    return this.http.get<Product[]>(url)
      .pipe(
        map(Products => Products[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? 'fetched' : 'did not find';
          this.log(`${outcome} Product id=${id}`);
        }),
        catchError(this.handleError<Product>(`getFeatureProductNo404 id=${id}`))
      );
  }

  /** GET FeatureProduct by id. Will 404 if id not found */
  getFeatureProducts(id: number): Observable<Product> {
    const url = `${this.productUrl}/${id}`;
    return this.http.get<Product>(url).pipe(
      tap(_ => this.log(`fetched FeatureProduct id=${id}`)),
      catchError(this.handleError<Product>(`getFeatureProduct id=${id}`))
    );
  }

  /* GET heroes whose name contains search term */
  searchProduct(term: string): Observable<Product[]> {
    if (!term.trim()) {
      // if not search term, return empty FeatureProduct array.
      return of([]);
    }
    return this.http.get<Product[]>(`${this.productUrl}/?name=${term}`).pipe(
      tap(x => x.length ?
         this.log(`found Product matching "${term}"`) :
         this.log(`no Product matching "${term}"`)),
      catchError(this.handleError<Product[]>('searchProduct', []))
    );
  }

  //Save methods

  /** POST: add a new FeatureProduct to the server */
  addProduct(Product: Product): Observable<Product> {
    return this.http.post<Product>(this.productUrl, Product, this.httpOptions).pipe(
      tap((newProduct: Product) => this.log(`added Product w/ id=${newProduct.id}`)),
      catchError(this.handleError<Product>('addProduct'))
    );
  }

  /** DELETE: delete the FeatureProduct from the server */
  deleteProduct(id: number): Observable<Product> {
    const url = `${this.productUrl}/${id}`;

    return this.http.delete<Product>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted Product id=${id}`)),
      catchError(this.handleError<Product>('deleteProduct'))
    );
  }

  /** PUT: update the FeatureProduct on the server */
  updateProduct(Product: Product): Observable<any> {
    return this.http.put(this.productUrl, Product, this.httpOptions).pipe(
      tap(_ => this.log(`updated Product id=${Product.id}`)),
      catchError(this.handleError<any>('updateProduct'))
    );
  }

  // Handle Http operation that failed.
 
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error); // log to console instead

      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  /** Log a ProductService message with the MessageService */
  private log(message: string) {
    //this.messageService.add(`ProductService: ${message}`);
  }
}
