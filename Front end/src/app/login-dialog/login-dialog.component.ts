import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

import { LoginService } from '../services/login.service';
import { LoginModel } from '../models/login-model';
import { Router } from '@angular/router';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent implements OnInit {

  emailForm: any;
  passwordForm: any;
  isLoading: boolean; 

  constructor(private dialogRef: MatDialogRef<LoginDialogComponent>, private loginService: LoginService, private router: Router) { }

  ngOnInit() {
    this.emailForm = new FormControl('',[Validators.required, Validators.pattern(EMAIL_REGEX)]);
    this.passwordForm = new FormControl('',[Validators.required]);
  }

  login(){
    this.isLoading = true;
    this.loginService.login(new LoginModel(this.emailForm.value, this.passwordForm.value))
.subscribe(result => {
      this.isLoading = false;
      sessionStorage.clear();
      sessionStorage.setItem('access_token', result.token);
      sessionStorage.setItem('role', result.role);
      sessionStorage.setItem('name', result.name);
      sessionStorage.setItem('userID', result.userID);

      this.router.navigate(['dashboard']);
  });    
    this.dialogRef.close();
  }

}
