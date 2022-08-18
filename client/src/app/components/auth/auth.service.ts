import { HttpClient, HttpClientModule } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "./user.model";
import { tap } from 'rxjs/operators';
import { Subject } from "rxjs";
import { Router } from "@angular/router";


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
    
    user = new Subject<User>();
    
    constructor(private http: HttpClient, private route: Router) {}
    
    signup(email: string, password: string) {
        return this.http.post<ResponseData>('https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyANoAmUcMTuzvDq2yRtsGO69_COMuDVZwg', {
            email: email,
            password: password,
            returnSecureToken: true
        }).pipe(tap(data => {
            this.authenticate(data.email, data.localId, data.idToken, data.expiresIn);
        }))
    }

    login(email: string, password: string) {
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
        this.route.navigate(['/home']);
    }

    private authenticate(email: string, id: string, token: string, expD: string) {
        const exp = new Date(new Date().getTime() + +expD * 1000); // milliseconds + tokenExp (seconds) * 1000 (convert to milliseconds) ** + in front of exp to convert to num
            const user = new User(email, id, token, exp);
            this.user.next(user);
            console.log(this.user);
    }
}