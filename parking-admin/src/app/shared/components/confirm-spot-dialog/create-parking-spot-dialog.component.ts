import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

import { ParkingSpotType } from '../../../core/enums/parking-spot-type.enum';

@Component({
  selector: 'app-create-parking-spot-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule, MatIconModule],
  templateUrl: './create-parking-spot-dialog.component.html',
})
export class CreateParkingSpotDialogComponent {
  isCreateModalOpen = false;

  @Input() open = false;
  @Input() defaultType!: ParkingSpotType;

  @Output() confirm = new EventEmitter<{ code: string; type: ParkingSpotType }>();
  @Output() close = new EventEmitter<void>();

  types = Object.values(ParkingSpotType)
  .filter(value => typeof value === 'number');

  spot = {
    code: '',
    type: null as ParkingSpotType | null,
  };

ParkingSpotType = ParkingSpotType;

  ngOnChanges() {
    this.spot.type = this.defaultType;
  }

  submit() {
    if (!this.spot.code || !this.spot.type) return;

    this.confirm.emit({
      code: this.spot.code,
      type: this.spot.type,
    });

    this.spot.code = '';
  }
}
