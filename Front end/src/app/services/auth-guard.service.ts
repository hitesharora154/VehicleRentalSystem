import { CanActivate, Router } from "@angular/router";
import { LoginService } from "./login.service";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private loginService: LoginService) { }

    canActivate() {
        if (sessionStorage.getItem('access_token')) {
            return true;
        }
        else {
            this.router.navigate(['']);
            return false;
        }
    }
}