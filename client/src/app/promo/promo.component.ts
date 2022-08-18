import { Component, OnInit } from '@angular/core';
import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-promo',
  templateUrl: './promo.component.html',
  styleUrls: ['./promo.component.css']
})
export class PromoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
