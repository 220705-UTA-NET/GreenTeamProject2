import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientInMemoryWebApiModule} from 'angular-in-memory-web-api';

import { InMemoryDataService } from './in-memory-data.service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material/material.module';
import { ClientListComponent } from './client-list/client-list.component';
import { ClientFormComponent } from './client-list/client-form/client-form.component';
import { FeatureProductComponent } from './feature-product/feature-product.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CartComponent } from './cart/cart.component';
import { FooterComponent } from './footer/footer.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { PromoComponent } from './promo/promo.component';
import { ProductSearchComponent } from './product-search/product-search.component';
import { AllProductsComponent } from './all-products/all-products.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { CarouselModule } from './carousel/carousel.module';

@NgModule({
  declarations: [
    AppComponent,
    ClientListComponent,
    ClientFormComponent,
    FeatureProductComponent,
    DashboardComponent,
    CartComponent,
    FooterComponent,
    ProductDetailsComponent,
    PromoComponent,
    ProductSearchComponent,
    AllProductsComponent,
    ContactUsComponent,
    AboutUsComponent,
    CheckoutComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CarouselModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
    HttpClientInMemoryWebApiModule.forRoot(InMemoryDataService, {dataEncapsulation: false}),
    RouterModule.forRoot([
      { path: '', component: FeatureProductComponent, pathMatch: 'full' }
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
