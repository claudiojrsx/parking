import { VehicleType } from "../enums/vehicle-type.enum";

export interface DailyActiveParkingDto {
  sessionId: string;
  vehicleId: string;
  plate: string;
  vehicleType: VehicleType;
  checkInAt: string;
}
