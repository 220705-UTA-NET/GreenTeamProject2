import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CategoryGridComponent } from './components/category-grid/category-grid.component';
import { MusicComponent } from './components/music/music.component';
import { HomeComponent } from './components/home/home.component';
import { FillNavbarComponent } from './components/fill-navbar/fill-navbar.component';
import { MusicItemComponent } from './components/music-item/music-item.component';

const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'navbar', component: NavBarComponent },
  { path: 'music', component: MusicComponent},
];

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    CategoryGridComponent,
    MusicComponent,
    HomeComponent,
    FillNavbarComponent,
    MusicItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
