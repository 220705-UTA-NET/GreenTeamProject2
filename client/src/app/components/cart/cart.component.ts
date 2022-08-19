import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/Product';
import { GlobalService } from 'src/app/shared/globalUser/globalUser.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  
  constructor(private globalUser: GlobalService) { }
  cart = this.globalUser.cart;
  // arr = [];
  // total = 0;


  ngOnInit(): void {
    // console.log(this.cart);
    // this.cart.forEach( (key, value) => {
      
    //   let p = value;
    //   console.log(p);
      
    //   this.arr.push( {
    //     id: key,
    //     name: value.productname
    //   } )
    // });
  }

}
