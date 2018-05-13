import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ViewBookingsComponent } from '../view-bookings/view-bookings.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  @ViewChild(ViewBookingsComponent) viewBookingComponent: ViewBookingsComponent;

  role: string;
  name: string;

  constructor(private router: Router) { }

  ngOnInit() {
    this.role = sessionStorage.getItem('role');
    this.name = sessionStorage.getItem('name');
  }

  logOut() {
    sessionStorage.clear();
    this.router.navigate(['']);
  }

  loadBookings() {
    this.viewBookingComponent.loadBooking();
  }

}
