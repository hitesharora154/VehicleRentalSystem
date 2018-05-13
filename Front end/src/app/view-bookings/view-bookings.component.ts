import { Component, OnInit } from '@angular/core';
import { BookingService } from '../services/booking.service';
import { Booking } from '../models/booking-model';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { DeleteDialogService } from '../delete-dialog/delete-dialog.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-view-bookings',
  templateUrl: './view-bookings.component.html',
  styleUrls: ['./view-bookings.component.css']
})
export class ViewBookingsComponent implements OnInit {

  dataSource = new BookingDataSource(this.bookingService);

  role: string;

  displayedColumns = ["name", "type", "bookingDate"];

  constructor(private bookingService: BookingService, private dialogService: DeleteDialogService, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.role = sessionStorage.getItem('role');
    if(this.role == 'Admin'){
      this.displayedColumns.unshift("username")
    }
  }

  loadBooking() {
    this.dataSource = new BookingDataSource(this.bookingService);
  }

  cancelBooking(bookingID) {
    this.bookingService.removeBooking(bookingID)
      .subscribe((result: any) => {
        if (result == 1) {
          this.snackBar.open("Booking Cancelled Successfully", "Yayy!", {
            duration: 3000
          });
        }
        else if (result == 0) {
          this.snackBar.open("Error in cancelling booking", "Ohh :(", {
            duration: 5000
          });
        }
        this.loadBooking();
      });
  }

  confirmDialog(bookingID, vehicleName, bookingDate) {
    this.dialogService.confirm('Confirmation Dialog', 'Are you sure you want to delete booking for ' + vehicleName + ' on ' + bookingDate + ' ?')
      .subscribe(res => {
        if (res == true) {
          this.cancelBooking(bookingID);
        }
      })
  }

}

export class BookingDataSource extends DataSource<any> {

  bookings: Booking[];

  constructor(private bookingService: BookingService) {
    super();
  }

  connect(): Observable<Booking[]> {
    return this.bookingService.getBookings();
  }
  disconnect(): void {
  }
}
