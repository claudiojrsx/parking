import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ParkingSpotService } from '../../core/services/parking-spot.service';
import { ParkingSpotType } from '../../core/enums/parking-spot-type.enum';

import { CreateParkingSpotDialogComponent } from '../../shared/components/confirm-spot-dialog/create-parking-spot-dialog.component';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { ParkingSpotSummary } from '../../core/models/parking-spot-summary.model';
import { ParkingSpot } from '../../core/models/parking-spot.model';

@Component({
  selector: 'app-parking-spots',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule,
    CreateParkingSpotDialogComponent,
  ],
  templateUrl: './parking-spots.component.html',
  styleUrls: ['./parking-spots.component.scss'],
})
export class ParkingSpotsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['code', 'type', 'status'];
  dataSource = new MatTableDataSource<ParkingSpot>([]);
  summary?: ParkingSpotSummary;

  selectedType: ParkingSpotType = ParkingSpotType.Car;
  ParkingSpotType = ParkingSpotType;

  newSpot = {
    code: '',
  };

  isCreateModalOpen = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private spotService: ParkingSpotService) {}

  ngOnInit(): void {
    this.loadSpots();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  loadSpots() {
    this.spotService.getAllAvailable(this.selectedType).subscribe((spots) => {
      this.dataSource.data = spots;
    });

    this.spotService.getSummary(this.selectedType).subscribe((summary) => {
      this.summary = summary;
    });
  }

  createSpot() {
    const payload = {
      code: this.newSpot.code,
      type: this.selectedType,
    };

    this.spotService.create(payload).subscribe(() => {
      this.newSpot.code = '';
      this.loadSpots();
    });
  }

  handleCreateSpot(data: { code: string; type: ParkingSpotType }) {
    this.spotService.create(data).subscribe(() => {
      this.isCreateModalOpen = false;
      this.loadSpots();
    });
  }
}
