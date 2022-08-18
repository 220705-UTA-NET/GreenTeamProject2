import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CategoryGridComponent } from './components/category-grid/category-grid.component';
import { ProductComponent } from './components/product/product.component';
import { ProductsComponent } from './components/products/products.component';
import { HomeComponent } from './components/home/home.component';
import { FillNavbarComponent } from './components/fill-navbar/fill-navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { AngularFireAuthModule } from '@angular/fire/compat/auth';
import { AngularFireModule } from '@angular/fire/compat';
import { environment } from 'src/environments/environment';
import { SigninComponent } from './components/signin/signin.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthComponent } from './components/auth/auth.component';
import { Spinner } from './shared/load-icon/spinner.component';
import { CartComponent } from './components/cart/cart.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';


const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'navbar', component: NavBarComponent },
  { path: 'products/:category', component: ProductsComponent},
  { path: 'signin', component: SigninComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'auth/:type', component: AuthComponent },

];

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    CategoryGridComponent,
    HomeComponent,
    FillNavbarComponent,
    ProductsComponent,
    ProductComponent,
    SigninComponent,
    SignupComponent,
    AuthComponent,
    Spinner,
    CartComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    AngularFireAuthModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
