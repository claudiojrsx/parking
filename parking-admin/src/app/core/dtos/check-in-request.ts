import { VehicleType } from '../enums/vehicle-type.enum';

export interface CheckInRequest {
  plate: string;
  vehicleType: VehicleType;
}
