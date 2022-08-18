import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from '../components/auth/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  private usersub: Subscription;

  constructor(private auth: AuthService) { }

  ngOnInit(): void {
    this.usersub = this.auth.user.subscribe();
  }

  onDestroy() {
    this.usersub.unsubscribe();
  }
}
