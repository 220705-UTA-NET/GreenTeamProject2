import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MockProducts } from 'src/app/MockProducts';
import { Product } from 'src/app/Product';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})

export class ProductsComponent implements OnInit {

  // fetch the products of the selected category from the api/database and set it equal to products (replace MockProducts)
  products: Product[];

  category: string = "";

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
  
    // set the category of the current route
    this.category = this.route.snapshot.params['category'];
    console.log(this.category);
    this.getProducts();
    // subscribe to callback so that if params is updated then the component view is updated
    // this.route.params.subscribe( (params: Params) => { this.category = params['category'] });
  }
  
  getProducts() {
    this.http.get(`https://green-api.azurewebsites.net/salesmanagement/products/${this.category}`).subscribe(products => { console.log(products)});
  }
}
