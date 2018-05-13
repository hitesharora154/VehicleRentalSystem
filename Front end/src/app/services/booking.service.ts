import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { MatSnackBar } from "@angular/material";
import { Observable } from 'rxjs/Observable';

import { Booking } from "../models/booking-model";
import { environment } from "../../environments/environment";

@Injectable()
export class BookingService {

    private apiUrl: string;

    private headers = new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });

    constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

    addBooking(booking: Booking) {
        return this.http.post(environment.apiUrl + '/api/booking', booking, { headers: this.headers })
            .catch((error: any) => {
                this.snackBar.open("Error in connecting to server", "Aww :(", {
                    duration: 5000
                })
                return Observable.throw(new Error(error));
            })
            .subscribe((result: any) => {
                if (result == 1) {
                    this.snackBar.open("Booking Done Successfully", "Yayy!", {
                        duration: 3000
                    });
                }
                else if (result == 0) {
                    this.snackBar.open("Error in adding booking", "Ohh :(", {
                        duration: 5000
                    });
                }
            });
    }

    getBookings(): Observable<Booking[]> {
        if (sessionStorage.getItem('role') == 'Admin') {
            this.apiUrl = environment.apiUrl + '/api/booking/all';
        }
        else {
            this.apiUrl = environment.apiUrl + '/api/booking/customer?userID=' + sessionStorage.getItem('userID');
        }

        return this.http.get<Booking[]>(this.apiUrl, { headers: this.headers })
            .map((bookingsArray: Array<any>) => {
                let bookingsResult: Array<Booking> = [];
                if (bookingsArray) {
                    bookingsArray.forEach((book) => {
                        bookingsResult.push(new Booking(
                            book.id,
                            book.vehicleID,
                            book.bookingDate,
                            book.user,
                            book.vehicle,
                            book.userID
                        ));
                    });
                }
                return bookingsResult;
            }
            );
    }

    removeBooking(bookingID){
        return this.http.delete(environment.apiUrl + '/api/booking?id='+bookingID, { headers: this.headers })
        .catch((error: any) => {
            this.snackBar.open("Error in connecting to server", "Aww :(", {
                duration: 5000
            })
            return Observable.throw(new Error(error));
        });
    }

}