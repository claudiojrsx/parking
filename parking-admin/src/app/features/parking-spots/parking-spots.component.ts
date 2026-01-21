import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ParkingSpotService } from '../../core/services/parking-spot.service';
import { ParkingSpotType } from '../../core/enums/parking-spot-type.enum';

@Component({
  selector: 'app-parking-spots',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './parking-spots.component.html',
  styleUrls: ['./parking-spots.component.scss']
})
export class ParkingSpotsComponent implements OnInit {

  spots: any[] = [];
  selectedType: ParkingSpotType = ParkingSpotType.Car;

  newSpot = {
    code: '',
    type: ParkingSpotType.Car
  };

  ParkingSpotType = ParkingSpotType; // expõe enum para o template

  constructor(private spotService: ParkingSpotService) {}

  ngOnInit(): void {
    this.loadSpots();
  }

  loadSpots() {
    this.spotService.getAllAvailable(this.selectedType)
      .subscribe(data => this.spots = data);
  }

  createSpot() {
    this.spotService.create(this.newSpot).subscribe(() => {
      this.newSpot = { code: '', type: this.selectedType };
      this.loadSpots();
    });
  }
}
