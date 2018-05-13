import { Injectable } from "@angular/core";
import { MatDialogRef, MatDialog } from "@angular/material";
import { Observable } from 'rxjs/Rx';

import { LoginDialogComponent } from "./login-dialog.component";

@Injectable()
export class LoginDialogService{

    constructor(private dialog: MatDialog){ }

    login(): Observable<boolean>{
        let dialogRef: MatDialogRef<LoginDialogComponent>;

        dialogRef = this.dialog.open(LoginDialogComponent);

        return dialogRef.afterClosed();
    }

}