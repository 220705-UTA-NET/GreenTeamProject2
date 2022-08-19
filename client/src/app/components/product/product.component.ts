import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/Product';
import { GlobalService } from 'src/app/shared/globalUser/globalUser.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})

export class ProductComponent implements OnInit {

  @Input() product: Product;
  
  constructor(private user: GlobalService) { }

  ngOnInit(): void {
  }

  addToCart() {
    
    this.user.cart.push(this.product);

    // console.log("clicked");
    // if(this.user.cart.has(this.product.id)) {
    //   this.user.cart.set(this.product.id, this.user.cart.get(this.product.id) + 1);
    // } else {
    //   this.user.cart.set(this.product.id, 1);
    // }
    console.log(this.user.cart);
    
    // call insert invoice post request
  }

}
