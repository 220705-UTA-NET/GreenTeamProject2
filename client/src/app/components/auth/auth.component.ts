import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { AuthService, ResponseData } from './auth.service';
import { Observable, Subject } from 'rxjs';
import { User } from './user.model';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  
  constructor(private route: ActivatedRoute, private authService: AuthService, private router: Router) { }
  loggedin: boolean;
  loading: boolean = false;
  error: string = null;
  
  ngOnInit(): void {
    this.loggedin = this.route.snapshot.params['type'] == 'login' ? true : false;
    this.route.params.subscribe( (params: Params) => {
      this.loggedin = params['type'] === 'login' ? true : false;
    });
  }

  changeView() {
    this.loggedin = !this.loggedin;
  }

  submitForm(form: NgForm) {
    if(!form.valid) return;
    const email = form.value.email;
    const password = form.value.password;
    
    let observe: Observable<ResponseData>;
 
    this.loading = true;
    
    if(this.loggedin) {
      observe = this.authService.login(email, password);
    } else {
      observe = this.authService.signup(email, password);
    }

    observe.subscribe(data => {
      console.log(data); this.loading = false;
      // upon login or singup, go to user cart page
      this.router.navigate(['/cart']);
    }, error => { 
      console.log(error); 
      this.loading = false; 
      this.error = "Authentication error occurred!"
    });
    
    form.reset();
  }

}
