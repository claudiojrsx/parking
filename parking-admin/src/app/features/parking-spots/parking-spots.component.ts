import {
  Component,
  OnInit,
  AfterViewInit,
  ViewChild
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ParkingSpotService } from '../../core/services/parking-spot.service';
import { ParkingSpotType } from '../../core/enums/parking-spot-type.enum';

import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';

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
  ],
  templateUrl: './parking-spots.component.html',
  styleUrls: ['./parking-spots.component.scss'],
})
export class ParkingSpotsComponent
  implements OnInit, AfterViewInit {

  /** 🔹 Angular Material */
  displayedColumns: string[] = ['code', 'type', 'status', 'actions'];
  dataSource = new MatTableDataSource<any>([]);

  /** 🔹 estado */
  selectedType: ParkingSpotType = ParkingSpotType.Car;
  ParkingSpotType = ParkingSpotType;

  /** 🔹 form */
  newSpot = {
    code: '',
  };

  /** 🔹 paginator e sort */
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

  /** 🔄 carregar vagas */
  loadSpots() {
    this.spotService
      .getAllAvailable(this.selectedType)
      .subscribe(spots => {
        this.dataSource.data = spots;
      });
  }

  /** ➕ criar vaga */
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
}
