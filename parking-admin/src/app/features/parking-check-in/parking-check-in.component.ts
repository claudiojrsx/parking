import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ParkingService } from '../../core/services/parking.service';
import { VehicleType } from '../../core/enums/vehicle-type.enum';

@Component({
  selector: 'app-parking-check-in',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './parking-check-in.component.html',
  styleUrls: ['./parking-check-in.component.scss'],
})
export class ParkingCheckInComponent {
  plate = '';
  vehicleType: VehicleType = VehicleType.Car;

  VehicleType = VehicleType;

  loading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private parkingService: ParkingService) {}

  submit() {
    this.successMessage = '';
    this.errorMessage = '';
    this.loading = true;

    this.parkingService
      .checkIn({
        plate: this.plate,
        vehicleType: this.vehicleType,
      })
      .subscribe({
        next: (res) => {
          this.successMessage = `Entrada registrada com sucesso! Sessão: ${res.sessionId}`;
          this.plate = '';
          this.vehicleType = VehicleType.Car;
        },
        error: (err) => {
          this.errorMessage =
            err?.error?.message || 'Erro ao realizar check-in';
        },
        complete: () => (this.loading = false),
      });
  }
}
