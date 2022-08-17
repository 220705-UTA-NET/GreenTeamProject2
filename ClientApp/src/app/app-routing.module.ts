import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { AllProductsComponent } from './all-products/all-products.component';
import { FeatureProductComponent } from './feature-product/feature-product.component';
import { PromoComponent } from './promo/promo.component';
import { ProductSearchComponent } from './product-search/product-search.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { ClientListComponent } from './client-list/client-list.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';

const routes: Routes = [ 
  { path: '', redirectTo: '/dashboard', pathMatch: 'full'},
  { path: 'dashboard', component: DashboardComponent},
  { path: 'allProducts', component: AllProductsComponent},
  { path: 'featureProducts', component: FeatureProductComponent},
  { path: 'promos', component: PromoComponent},
  { path: 'search', component: ProductSearchComponent},
  { path: 'aboutUs', component: AboutUsComponent},
  { path: 'contactUs', component: ContactUsComponent},
  { path: 'client', component: ClientListComponent},
  { path: 'cart', component: CartComponent},
  { path: 'checkout', component: CheckoutComponent}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
