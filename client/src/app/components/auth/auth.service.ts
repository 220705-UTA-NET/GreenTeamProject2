import { HttpClient, HttpClientModule } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "./user.model";
import { tap } from 'rxjs/operators';
import { Subject } from "rxjs";
import { Router } from "@angular/router";
import { GlobalService } from "src/app/shared/globalUser/globalUser.service";



export interface ResponseData {
    kind: string,
    idToken: string,
    email: string,
    refreshToken: string,
    expiresIn: string,
    localId: string,
    registered?: boolean
}


@Injectable({providedIn: 'root'})
export class AuthService {
    
    firstSignup: boolean;
    user = new Subject<User>();
    
    constructor(private http: HttpClient, private route: Router, private globaluser: GlobalService) {}
    
    signup(email: string, password: string) {
        this.firstSignup = true;
        return this.http.post<ResponseData>('https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyANoAmUcMTuzvDq2yRtsGO69_COMuDVZwg', {
            email: email,
            password: password,
            returnSecureToken: true
        }).pipe(tap(data => {
            this.authenticate(data.email, data.localId, data.idToken, data.expiresIn);
        }))
    }

    login(email: string, password: string) {
        this.firstSignup = false;
        return this.http.post<ResponseData>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyANoAmUcMTuzvDq2yRtsGO69_COMuDVZwg", {
            email: email,
            password: password,
            returnSecureToken: true
        }).pipe(tap(data => {
            this.authenticate(data.email, data.localId, data.idToken, data.expiresIn);
        }))
    }

    logout() {
        this.user.next(null);
        this.globaluser.cart = [];
        this.route.navigate(['/home']);
    }

    private authenticate(email: string, id: string, token: string, expD: string) {
        const exp = new Date(new Date().getTime() + +expD * 1000); // milliseconds + tokenExp (seconds) * 1000 (convert to milliseconds) ** + in front of exp to convert to num

            
            if(this.firstSignup) {
                
            } else {
                //get users info/make get request
                // const currentUser = new UserService()
            }
            
            const user = new User(email, id, token, exp);
            this.user.next(user);
            console.log(this.user);
    }
}