import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './product';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  items: Product[] = [];

  constructor(private http: HttpClient) { }

  addToCart(product: Product) {
    this.items.push(product);
  }
  getItems() {
    return this.items;
  }

  clearCart() {
    this.items=[];
    return this.items;
  }

  getPromo() {
    return this.http.get<{type: string, desc: string}[]>
    ('/assets/promo.json')
  }
}
