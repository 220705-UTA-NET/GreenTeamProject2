import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})

export class ProductDetailsComponent implements OnInit {
  product: Product | undefined;
  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getFeatureProducts();
  }
  getFeatureProducts(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.productService.getFeatureProducts(id)
      .subscribe(Product => this.product = Product);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    //if (this.product) {
      //this.featureProductService.updateProduct(this.product)
        //.subscribe(() => this.goBack());
    //}
  }
}
