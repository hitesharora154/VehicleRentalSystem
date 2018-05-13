import { VehicleType } from "./vehicle-type";

export class Vehicle {
    id: string;
    name: string;
    typeID: string;
    vehicleType: VehicleType;

    constructor(id: string, name: string, typeID: string, vehicleType: VehicleType) {
        if (id == null && vehicleType == null) {
            this.name = name;
            this.typeID = typeID;
        }
        else {
            this.id = id;
            this.name = name;
            this.typeID = typeID;
            this.vehicleType = new VehicleType(vehicleType.id, vehicleType.name);
        }
    }
}