import { Injectable } from '@angular/core';
import { Product } from 'src/app/Product';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {

  public cust_id: number;
  public username: string;
  public name: string;
  public email: string;
  public token: string;
  public cart = new Map();
  
}

