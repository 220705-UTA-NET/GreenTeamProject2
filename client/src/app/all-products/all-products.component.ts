import { Component, OnInit } from '@angular/core';
import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.css']
})

export class AllProductsComponent implements OnInit {
  allProducts: Product[] = [];

  constructor(private ProductService: ProductService) { }

  ngOnInit(): void {
    this.getAllProducts();
  }

  Cart: number[] = [] 
  addProductToCart(product:number){
    this.Cart.push(product);
    console.log(product);
  }
  
  getAllProducts(): void{
    this.ProductService.getAllProducts()
    .subscribe(allProducts => this.allProducts = allProducts);
  }
}
