import { Component, OnInit } from '@angular/core';
import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  allProducts: Product[] = [];

  constructor(private ProductService: ProductService) { }

  ngOnInit(): void {
    //this.getAllProducts();
    //this.allProducts.push(this.product);
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
