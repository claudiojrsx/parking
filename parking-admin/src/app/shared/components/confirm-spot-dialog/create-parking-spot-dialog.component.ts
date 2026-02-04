import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ParkingSpotType } from '../../../core/enums/parking-spot-type.enum';

@Component({
  selector: 'app-create-parking-spot-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-parking-spot-dialog.component.html',
})
export class CreateParkingSpotDialogComponent {
  isCreateModalOpen = false;

  @Input() open = false;
  @Input() defaultType!: ParkingSpotType;

  @Output() confirm = new EventEmitter<{ code: string; type: ParkingSpotType }>();
  @Output() close = new EventEmitter<void>();

  types = Object.values(ParkingSpotType);

  spot = {
    code: '',
    type: null as ParkingSpotType | null,
  };

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
