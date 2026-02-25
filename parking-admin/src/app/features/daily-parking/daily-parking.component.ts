import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { DailyParkingService } from '../../core/services/daily-parking.service';
import { ActiveParkingDto } from '../../core/dtos/active-parking.dto';
import { VehicleType } from '../../core/enums/vehicle-type.enum';

@Component({
  selector: 'app-daily-parking',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './daily-parking.component.html',
})
export class DailyParkingComponent implements OnInit {

  VehicleType = VehicleType;

  plate = '';
  vehicleType!: VehicleType;

  activeParkings: ActiveParkingDto[] = [];
  loading = false;
  successMessage = '';
  errorMessage = '';

  constructor(private service: DailyParkingService) {}

  ngOnInit(): void {
    this.loadActive();
  }

  loadActive(): void {
    this.service.getActive().subscribe({
      next: data => this.activeParkings = data,
      error: () => this.errorMessage = 'Erro ao carregar veículos ativos.'
    });
  }

  registerEntry(): void {

    if (!this.plate || !this.vehicleType) return;

    this.loading = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.service.registerEntryByPlate(this.plate, this.vehicleType)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => {
          this.successMessage = 'Veículo registrado com sucesso!';
          this.plate = '';
          this.vehicleType = undefined as any;
          this.loadActive();
        },
        error: () => {
          this.errorMessage = 'Erro ao registrar entrada.';
        }
      });
  }

  registerExit(id: string): void {

    this.successMessage = '';
    this.errorMessage = '';

    this.service.registerExit(id).subscribe({
      next: (total) => {
        this.successMessage = `Saída registrada. Total: R$ ${total}`;
        this.loadActive();
      },
      error: () => {
        this.errorMessage = 'Erro ao registrar saída.';
      }
    });
  }
}
