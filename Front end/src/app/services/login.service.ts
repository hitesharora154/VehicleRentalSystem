import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";

import { LoginModel } from "../models/login-model";
import { environment } from '../../environments/environment';
import { TokenReponse } from "../models/token-response-model";
import { MatSnackBar } from "@angular/material";
import { Observable } from "rxjs/observable";

@Injectable()
export class LoginService{

    isLoading: boolean;

    constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar) { 
        this.isLoading = true;
    }

    login(userInfo: LoginModel){
       return this.http.post<TokenReponse>(environment.apiUrl + "/api/token", userInfo)
        .catch((error: any) => {
            this.snackBar.open("Invalid Credentials", "Whaaatt", {
                duration: 5000
            })
            return Observable.throw(new Error(error));
        });
    }
}