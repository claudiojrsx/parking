import { ParkingSpotType } from '../enums/parking-spot-type.enum';

export interface ParkingSpot {
  id: string;
  code: string;
  type: ParkingSpotType;
  isOccupied: boolean;
  isActive: boolean;
}
