import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response, Headers } from '@angular/http'
import { MatSnackBar } from '@angular/material';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { Vehicle } from '../models/vehicle';
import { isNgTemplate } from '@angular/compiler';
import { VehicleType } from '../models/vehicle-type';

@Injectable()
export class VehicleService {

  private headers = new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  getAllVehicles(typeId) {
    return this.http.get<Vehicle[]>(environment.apiUrl + '/api/vehicle?vehicleTypeId=' + typeId, { headers: this.headers });
  }

  getVehicleTypes() {
    return this.http.get<VehicleType[]>(environment.apiUrl + '/api/vehicletype', { headers: this.headers });
  }

  addVehicle(vehicle: Vehicle) {
    return this.http.post(environment.apiUrl + '/api/vehicle', vehicle, { headers: this.headers })
      .catch((error: any) => {
        this.snackBar.open("Error in connecting to server", "Aww :(",{
          duration:5000
        })
        return Observable.throw(new Error(error));
      })
      .subscribe((result: any) => {
        if (result == 1) {
          this.snackBar.open("Vehicle Added Successfully", "Yayy!", {
            duration: 3000
          })
        }
        else if (result == 0) {
          this.snackBar.open("Error in Adding Vehicle", "Ohh :(", {
            duration: 5000
          })
        }
      });
  }
}
