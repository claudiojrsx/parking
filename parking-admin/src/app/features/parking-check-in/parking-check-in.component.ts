import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs/operators';

import { ParkingService } from '../../core/services/parking.service';
import { VehicleType } from '../../core/enums/vehicle-type.enum';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-parking-check-in',
  standalone: true,
  imports: [CommonModule, FormsModule, ConfirmDialogComponent],
  templateUrl: './parking-check-in.component.html',
})
export class ParkingCheckInComponent {
  plate = '';
  vehicleType: VehicleType = VehicleType.Car;

  VehicleType = VehicleType;

  confirmOpen = false;

  loading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private parkingService: ParkingService) {}

  /** Abre o modal de confirmação */
  openConfirm() {
    if (!this.plate) return; // evita abrir vazio
    this.successMessage = '';
    this.errorMessage = '';
    this.confirmOpen = true;
  }

  /** Usuário confirmou */
  handleConfirm() {
    this.confirmOpen = false;
    this.submit();
  }

  private submit() {
    this.loading = true;

    this.parkingService
      .checkIn({
        plate: this.plate.toUpperCase(),
        vehicleType: this.vehicleType,
      })
      .pipe(
        finalize(() => {
          this.loading = false; // garante reset
        }),
      )
      .subscribe({
        next: (res) => {
          this.successMessage = `Entrada registrada! Sessão: ${res.sessionId}`;
          this.plate = '';
          this.vehicleType = VehicleType.Car;
        },
        error: (err) => {
          this.errorMessage =
            err?.error?.message || 'Erro ao realizar check-in';

          this.confirmOpen = false;
        },
      });
  }
}
