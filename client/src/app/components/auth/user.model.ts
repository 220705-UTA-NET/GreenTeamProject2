export class User {
    

    public Email: string;
    public Username: string;
    public Password: string = "";
    public Address: string;
    public Phone: string;
    private _Token: string;
    private _TokenExp: Date;
    public Id: string;
    public dbId: number;
    
    constructor(public email: string, public id: string, private _token: string, private _tokenExp: Date) {
        this.Email = email;
        this.Id = id;
        this._Token = _token;
        this._tokenExp = _tokenExp;
    }

    get token() {
        
        if(!this._tokenExp || this._tokenExp < new Date()) {
            return null;
        }
        return this._token;
    }

}