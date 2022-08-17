import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Product } from './product';

@Injectable({
  providedIn: 'root',
})
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const Products = [
      { id: 102, name: 'NFT 102', price: 20 },
      { id: 103, name: 'NFT 103', price: 30 },
      { id: 105, name: 'NFT 105', price: 40 },
      { id: 106, name: 'NFT 106', price: 50 },
      { id: 107, name: 'NFT 107', price: 60 },
      { id: 108, name: 'NFT 108', price: 70 },
      { id: 109, name: 'NFT 109', price: 80 },
      { id: 110, name: 'NFT 110', price: 90 }
    ];
    return {Products};
  }

//Assigns an ID to the product.
  genId(products: Product[]): number {
    return products.length > 0 ? Math.max(...products.map(products => products.id)) + 1 : 101;
  }
}
