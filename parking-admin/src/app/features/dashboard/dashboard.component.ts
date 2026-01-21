import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParkingSpotService } from '../../core/services/parking-spot.service';
import { ParkingSpotType } from '../../core/enums/parking-spot-type.enum';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {

  totalSpots = 0;
  freeSpots = 0;

  selectedType: ParkingSpotType = ParkingSpotType.Car;
  ParkingSpotType = ParkingSpotType;

  constructor(private spotService: ParkingSpotService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.loadTotal();
    this.loadFree();
  }

  loadTotal() {
    this.spotService.getAllByType(this.selectedType)
      .subscribe(spots => this.totalSpots = spots.length);
  }

  loadFree() {
    this.spotService.getAllAvailable(this.selectedType)
      .subscribe(spots => this.freeSpots = spots.length);
  }

  changeType(type: ParkingSpotType) {
    this.selectedType = type;
    this.loadData();
  }
}
