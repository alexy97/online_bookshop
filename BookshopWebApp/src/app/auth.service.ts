import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient) { }
    isAdmin() {
        //In reality would be checking the role of the user from an endpoint in the API
        return true;
    }
}
