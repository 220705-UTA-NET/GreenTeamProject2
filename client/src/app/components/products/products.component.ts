import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MockProducts } from 'src/app/MockProducts';
import { Product } from 'src/app/Product';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})

export class ProductsComponent implements OnInit {

  // fetch the products of the selected category from the api/database and set it equal to products (replace MockProducts)
  products: Product[];
  loading: boolean = false;
  category: string = "";

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    
    // set the category of the current route
    this.category = this.route.snapshot.params['category'];
    console.log(this.category);
    
    if(this.category == "ALL") {
      this.getAllProducts();
    } else {
      this.getProducts();
    }
    // subscribe to callback so that if params is updated then the component view is updated
    // this.route.params.subscribe( (params: Params) => { this.category = params['category'] });
  }

  getAllProducts() {
    this.loading = true;
    this.http.get(`https://green-api.azurewebsites.net/salesmanagement/getallproducts`).pipe(map(responseData => {
      let arr = [];
      for(const o in responseData) {
        arr.push({...responseData[o]});
      }
      return arr;
    })).subscribe(products => { console.log(typeof(products)); console.log(products); this.products = products; this.loading = false;});
  }
  
  getProducts() {
    this.loading = true;
    this.http.get(`https://green-api.azurewebsites.net/salesmanagement/products/${this.category}`).pipe(map(responseData => {
      let arr = [];
      for(const o in responseData) {
        arr.push({...responseData[o]});
      }
      return arr;
    })).subscribe(products => { console.log(typeof(products)); console.log(products); this.products = products; this.loading = false;});
  }
}

