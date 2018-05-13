import { Injectable } from "@angular/core";
import { MatDialogRef, MatDialog } from "@angular/material";
import { Observable } from 'rxjs/Rx';

import { RegisterDialogComponent } from "./register-dialog.component";

@Injectable()
export class RegisterDialogService{

    constructor(private dialog: MatDialog){ }

    register(): Observable<boolean>{
        let dialogRef: MatDialogRef<RegisterDialogComponent>;

        dialogRef = this.dialog.open(RegisterDialogComponent);

        return dialogRef.afterClosed();
    }

}