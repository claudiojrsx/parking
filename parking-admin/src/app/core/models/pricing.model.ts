export type VehicleType = 'CAR' | 'MOTORCYCLE' | 'TRUCK';

export interface Pricing {
  id?: number;

  vehicleType: VehicleType;

  pricePerHour: number;
  pricePerDay: number;

  toleranceMinutes: number; // ex: minutos grátis
  active: boolean;
}
