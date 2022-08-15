import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CategoryGridComponent } from './components/category-grid/category-grid.component';
import { MusicComponent } from './components/music/music.component';

const appRoutes: Routes = [
  { path: 'navbar', component: NavBarComponent },
  { path: 'music', component: MusicComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    CategoryGridComponent,
    MusicComponent
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
