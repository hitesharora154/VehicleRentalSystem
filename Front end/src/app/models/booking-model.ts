import { User } from "./user-model";
import { Vehicle } from "./vehicle";

export class Booking {
    id: string;
    vehicleID: string;
    bookingDate: Date;
    userID: string;
    user: User;
    vehicle: Vehicle;

    constructor(id: string, vehicleID: string, bookingDate: Date, user: User, vehicle: Vehicle, userID: string) {
        if (id == null && user == null && vehicle == null) {
            this.bookingDate = bookingDate;
            this.vehicleID = vehicleID;
            this.userID = userID;
        }
        else {
            this.id = id;
            this.vehicleID = vehicleID;
            this.bookingDate = bookingDate;
            this.user = user;
            this.userID = userID;
            this.vehicle = vehicle
        }
    }
}