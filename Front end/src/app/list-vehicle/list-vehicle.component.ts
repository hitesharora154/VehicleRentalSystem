import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { VehicleService } from '../services/vehicle.service';
import { Vehicle } from '../models/vehicle';
import { VehicleType } from '../models/vehicle-type';

@Component({
  selector: 'app-list-vehicle',
  templateUrl: './list-vehicle.component.html',
  styleUrls: ['./list-vehicle.component.css']
})
export class ListVehicleComponent implements OnInit {
  
  vehicleTypes: VehicleType[];
  vehicleNameForm: any;
  vehicleTypeForm: any;
  isTypeSelected: boolean;
  typeName: string;

  constructor(private vehicleService: VehicleService) {
    this.vehicleTypes = new Array<VehicleType>();
  }

  ngOnInit() {
    this.vehicleService.getVehicleTypes().subscribe(
      data => {
        for (let vehicleType of data) {
          let type = new VehicleType(
            vehicleType.id,
            vehicleType.name
          );
          this.vehicleTypes.push(type);
        }
      }
    );

    this.isTypeSelected = false;
    this.vehicleTypeForm = new FormControl('', [Validators.required]);
    this.vehicleNameForm = new FormControl('', [Validators.required]);
  }

  add(){
    this.vehicleService.addVehicle(new Vehicle(null, this.vehicleNameForm.value, this.typeName, null));
    this.vehicleNameForm.reset();
    this.vehicleTypeForm.reset();
    this.isTypeSelected = false;
  }

  typeSelected(typeName: string){
    this.typeName = typeName;
    this.isTypeSelected = true;
  }

}
