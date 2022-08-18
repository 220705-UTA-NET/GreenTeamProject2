import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {

    constructor(id: number, username: string, password: string, email: string, address: string, phone: string, token: string) {}

}