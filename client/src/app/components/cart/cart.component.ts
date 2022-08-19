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
  total = 0;
  // arr = [];
  // total = 0;
  
  
  ngOnInit(): void {
    
    this.cart.forEach(element => {
      this.total += element.unitprice;
    });

    this.cart = this.cart.filter( (value, index, self) =>
      index === self.findIndex( elem => (
      elem.id === value.id
    )))
    
    console.log(this.total);
    console.log(this.cart);
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
