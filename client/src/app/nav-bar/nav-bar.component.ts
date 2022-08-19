import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../components/auth/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit, OnDestroy {

  private usersub: Subscription;
  userExists = false;
  constructor(private auth: AuthService, private route: Router) { }

  ngOnInit(): void {
    this.usersub = this.auth.user.subscribe(user => { this.userExists = user ? true : false;});
    
  }

  ngOnDestroy() {
    this.usersub.unsubscribe();
  }

  logout() {
    this.auth.logout();
  }
}
