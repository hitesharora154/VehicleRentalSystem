import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { RegisterModel } from "../models/register-model";
import { environment } from '../../environments/environment';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class RegisterService {

  private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  RegisterUser(userInfo: RegisterModel) {
    userInfo.role = 'Customer';
    return this.http.post(environment.apiUrl + "/api/account", userInfo, { headers: this.headers })
      .subscribe(res => {
        if(res == 1){
          this.snackBar.open("Registration Successful, Please Log in to continue", "Ok :)", {
            duration: 3000
          })
        }
        else{
          this.snackBar.open("Error in registration","Aww :(", {
            duration: 5000
          })
        }
      });
  }

}
