import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

// Importações do Angular Material
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';

import { DailyParkingService } from '../../core/services/daily-parking.service';
import { DailyActiveParkingDto } from '../../core/dtos/daily-parking.dto';
import { VehicleType } from '../../core/enums/vehicle-type.enum';

@Component({
  selector: 'app-daily-parking',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule
  ],
  templateUrl: './daily-parking.component.html',
  styleUrls: ['./daily-parking.component.scss'],
})
export class DailyParkingComponent implements OnInit {
  // Configuração das colunas da tabela
  displayedColumns: string[] = ['plate', 'type', 'entry', 'actions'];

  VehicleType = VehicleType;
  plate = '';
  vehicleType!: VehicleType;

  activeParkings: DailyActiveParkingDto[] = [];
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
    if (!this.plate || this.vehicleType === undefined) return;

    this.loading = true;
    this.clearMessages();

    this.service.registerEntryByPlate(this.plate, this.vehicleType)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => {
          this.successMessage = 'Veículo registrado com sucesso!';
          this.resetForm();
          this.loadActive();
        },
        error: () => this.errorMessage = 'Erro ao registrar entrada.'
      });
  }

  registerExit(id: string): void {
    this.clearMessages();

    this.service.registerExit(id).subscribe({
      next: (total) => {
        this.successMessage = `Saída registrada. Total: R$ ${total.toFixed(2)}`;
        this.loadActive();
      },
      error: () => this.errorMessage = 'Erro ao registrar saída.'
    });
  }

  // Métodos auxiliares para manter o código limpo
  private clearMessages(): void {
    this.successMessage = '';
    this.errorMessage = '';
  }

  private resetForm(): void {
    this.plate = '';
    this.vehicleType = undefined as any;
  }
}
