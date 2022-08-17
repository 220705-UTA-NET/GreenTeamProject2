export class clientLogin{
    customer_id: number;
    username: string;
    password: string;
    name: string;
    address: string;
    phone: string;
    email: string;

    constructor( customer_id: number, username: string, password: string, name: string, address: string, phone: string, email: string){
        this.customer_id = customer_id;
        this.username = username;
        this.password = password;
        this.name = name;
        this.address = address;
        this.phone = phone;
        this.email = email;
    }
}