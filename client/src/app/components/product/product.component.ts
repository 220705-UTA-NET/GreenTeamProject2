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

  updateCart() {

    if(this.product.id in this.user.cart) {
      this.user.cart[this.product.id]++;
    } else {
      this.user.cart[this.product.id] = 1;
    }
    
  }

}
