import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParkingSpotService } from '../../core/services/parking-spot.service';
import { ParkingSpotType } from '../../core/enums/parking-spot-type.enum';
import { ParkingSpotSummary } from '../../core/models/parking-spot-summary.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {

  selectedType: ParkingSpotType = ParkingSpotType.Car;
  summary?: ParkingSpotSummary;

  ParkingSpotType = ParkingSpotType;

  constructor(private spotService: ParkingSpotService) {}

  ngOnInit(): void {
    this.loadSummary();
  }

  changeType(type: ParkingSpotType) {
    this.selectedType = type;
    this.loadSummary();
  }

  loadSummary() {
    this.spotService
      .getSummary(this.selectedType)
      .subscribe(summary => this.summary = summary);
  }
}
