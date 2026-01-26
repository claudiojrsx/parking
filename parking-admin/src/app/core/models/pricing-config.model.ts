// src/app/core/models/pricing.model.ts

export type VehicleType = 'CAR' | 'MOTORCYCLE' | 'TRUCK';

export interface Pricing {
  id?: number;
  vehicleType: VehicleType;

  pricePerHour: number;
  pricePerDay: number;

  toleranceMinutes: number; // ex: 10 min grátis
  active: boolean;
}
