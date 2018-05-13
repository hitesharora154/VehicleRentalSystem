import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { RegisterService } from '../services/register.service';
import { RegisterModel } from '../models/register-model';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/;

@Component({
  selector: 'app-register-dialog',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.css']
})
export class RegisterDialogComponent implements OnInit {

  constructor(private dialogRef: MatDialogRef<RegisterDialogComponent>, private registerService: RegisterService) { }

  nameForm: any;
  emailForm: any;
  passwordForm: any;
  confirmPasswordForm: any;

  password: any;
  confirmPassword: any;

  ngOnInit() {
    this.emailForm = new FormControl('', [Validators.required, Validators.pattern(EMAIL_REGEX)]);
    this.nameForm = new FormControl('', Validators.required);
    this.passwordForm = new FormControl('', [Validators.required, Validators.pattern(PASSWORD_REGEX)]);
    this.confirmPasswordForm = new FormControl('', [Validators.required]);
  }

  register() {
    this.confirmPasswordForm.reset();
    this.registerService.RegisterUser(new RegisterModel(
      this.emailForm.value,
      this.passwordForm.value,
      this.nameForm.value,
      ''));
    this.emailForm.reset();
    this.passwordForm.reset();
    this.nameForm.reset();
    this.dialogRef.close();
  }

}
