import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { VehicleService } from '../services/vehicle.service';
import { VehicleType } from '../models/vehicle-type';
import { Vehicle } from '../models/vehicle';
import { BookingService } from '../services/booking.service';
import { Booking } from '../models/booking-model';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.css']
})
export class AddBookingComponent implements OnInit {

  datePickerForm: any;
  vehicleTypeForm: any;
  vehicleNameForm: any;
  typePicked: boolean;
  namePicked: boolean;
  vehicleTypes: VehicleType[];
  vehicleNames: Vehicle[];
  vehicleId: any;

  dateFilter = (d: Date): boolean => {
    let currentDate = new Date();

    if (d <= currentDate) {
      return false;
    }
    return true;
  }

  constructor(private vehicleService: VehicleService, private bookingService: BookingService) { }

  ngOnInit() {
    this.vehicleService.getVehicleTypes().subscribe(data => {
      this.vehicleTypes = data;
    })

    this.vehicleTypeForm = new FormControl('', [Validators.required]);
    this.vehicleNameForm = new FormControl('', [Validators.required]);
    this.datePickerForm = new FormControl('', [Validators.required]);
  }

  Book(){
    this.bookingService.addBooking(new Booking(null, this.vehicleId, this.datePickerForm.value, null, null, sessionStorage.getItem('userID')));
    this.datePickerForm.reset();
    this.vehicleNameForm.reset();
    this.vehicleTypeForm.reset();
    this.typePicked = false;
    this.namePicked = false;
  }

  typeSelected(typeName){
    this.typePicked = true;
    this.vehicleService.getAllVehicles(typeName).subscribe(data => {
      this.vehicleNames = data;
    })
  }

  nameSelected(vehicleName){
    this.namePicked = true;
    this.vehicleId = vehicleName;
  }

}
