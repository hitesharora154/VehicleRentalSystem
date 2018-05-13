import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html'
})
export class DeleteDialogComponent implements OnInit {

  title: string;
  message: string;

  constructor(private dialogRef: MatDialogRef<DeleteDialogComponent>) { }

  ngOnInit() {
  }

}
