import { Component, OnInit } from '@angular/core';
import { LoginDialogService } from '../login-dialog/login-dialog.service';
import { RegisterDialogService } from '../register-dialog/register-dialog.service';

@Component({
  selector: 'app-home-screen',
  templateUrl: './home-screen.component.html',
  styleUrls: ['./home-screen.component.css']
})
export class HomeScreenComponent implements OnInit {

  constructor(private loginDialog: LoginDialogService, private registerDialog: RegisterDialogService) { }

  ngOnInit() {
  }

  login(){
    this.loginDialog.login();
  }

  register(){
    this.registerDialog.register();
  }

}
