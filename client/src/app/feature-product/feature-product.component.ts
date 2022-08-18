import { Component, OnInit } from '@angular/core';
import { Product } from '../product';
import { ProductService} from '../product.service';

@Component({
  selector: 'app-feature-product',
  templateUrl: './feature-product.component.html',
  styleUrls: ['./feature-product.component.css']
})
export class FeatureProductComponent implements OnInit {
  featureProducts: Product[]= [];
  constructor(private ProductService: ProductService) { }

  ngOnInit(): void {
    this.getFeatureProducts();
  }
  getFeatureProducts(): void {
    this.ProductService.getAllProducts()
    .subscribe(featureProducts => this.featureProducts = featureProducts.slice(1,4));
  }
}
