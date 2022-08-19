import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { AuthService, ResponseData } from './auth.service';
import { Observable, Subject } from 'rxjs';
import { User } from './user.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from 'src/app/Customer';
import { GlobalService } from 'src/app/shared/globalUser/globalUser.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  
  constructor(private globaluser: GlobalService, private route: ActivatedRoute, private authService: AuthService, private router: Router, private http: HttpClient) { }
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
    
    const saveForm = JSON.stringify(form.value);
    
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
      // console.log(data);
      this.loading = false;
      
      if(!this.loggedin) {
        // signup
        this.createUserDB(saveForm, data);
      } else {
        this.getUserInfoFromDB();
      }

      // upon login or singup, go to user cart page
      this.router.navigate(['/cart']);
    }, error => { 
      console.log(error); 
      this.loading = false; 
      this.error = "Authentication error occurred!"
    });
    
    form.reset();
  }

  createUserDB(form: string, data: ResponseData) {
    const newForm = JSON.parse(form);
    console.log("form value in createuserdb:")
    console.log(newForm);
    
    const headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    const body = {
      id: 0,
      username: newForm.username,
      password: newForm.password,
      email: newForm.email,
      name: newForm.name,
      address: newForm.address,
      phoneNumber: newForm.phone,
      token: data.idToken
    }

    console.log(body);

    this.http.post<any>('https://green-api.azurewebsites.net/User/SignupUser', JSON.stringify(body), headers).subscribe(responseData => {
      console.log(responseData);
      // this.globaluser.email = responseData.email
    }, error => {
      console.log(error);
    });
  }

  getUserInfoFromDB() {

  }

}
