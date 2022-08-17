import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MockProducts } from 'src/app/MockProducts';
import { Product } from 'src/app/Product';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

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

    // subscribe to callback so that if params is updated then the component view is updated
    // this.route.params.subscribe( (params: Params) => { this.category = params['category'] });

    
    switch(this.category) {

      case "all":
        // get all products of this category
        // this.products = result from http get request
        this.http.get().subscribe(products => { console.log(products)})
        break;
      case "music":

        break;
      case "nft":

        break;
      case "document":

        break;
      case "videogame":

        break;
      case "audiobook":

        break;
      
    }
    
  
  }

}
