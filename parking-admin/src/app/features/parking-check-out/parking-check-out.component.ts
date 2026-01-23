import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';

import { ParkingService } from '../../core/services/parking.service';

@Component({
  selector: 'app-parking-check-out',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatChipsModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
  ],
  templateUrl: './parking-check-out.component.html',
  styleUrls: ['./parking-check-out.component.scss'],
})
export class ParkingCheckOutComponent {
  displayedColumns = ['plate', 'type', 'checkIn', 'action'];

  dataSource = new MatTableDataSource<any>([]);

  loading = false;
  result: any | null = null;
  errorMessage = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private parkingService: ParkingService) {}

  ngOnInit() {
    this.loadActiveSessions();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadActiveSessions() {
    this.parkingService.getActiveSessions().subscribe({
      next: (sessions) => {
        this.dataSource.data = sessions;
      },
    });
  }

  checkOut(vehicleId: string) {
    this.loading = true;
    this.errorMessage = '';
    this.result = null;

    this.parkingService.checkOut(vehicleId).subscribe({
      next: (res) => {
        this.result = res;
        this.loadActiveSessions();
      },
      error: (err) => {
        this.errorMessage = err?.error?.message || 'Erro no check-out';
      },
      complete: () => (this.loading = false),
    });
  }
}
