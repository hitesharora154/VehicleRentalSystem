import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { RegisterService } from '../services/register.service';
import { RegisterModel } from '../models/register-model';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
const PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  nameForm: any;
  emailForm: any;
  passwordForm: any;
  confirmPasswordForm: any;

  constructor(private registerService: RegisterService) { }

  ngOnInit() {
    this.nameForm = new FormControl('', [Validators.required]);
    this.emailForm = new FormControl('', [Validators.required, Validators.pattern(EMAIL_REGEX)]);
    this.passwordForm = new FormControl('', [Validators.required, Validators.pattern(PASSWORD_REGEX)]);
    this.confirmPasswordForm = new FormControl('', [Validators.required]);
  }

  register(){
    let registerModel = new RegisterModel(
      this.emailForm.value,
      this.passwordForm.value,
      this.nameForm.value,
      null
    );
    this.registerService.RegisterUser(registerModel);
    this.nameForm.reset();
    this.emailForm.reset();
    this.passwordForm.reset();
    this.confirmPasswordForm.reset();
  }

}
