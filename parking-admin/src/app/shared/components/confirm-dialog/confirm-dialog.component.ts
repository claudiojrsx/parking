import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './confirm-dialog.component.html',
})
export class ConfirmDialogComponent {
  @Input() open = false;
  @Input() title = 'Tem certeza?';
  @Input() description = '';
  @Input() confirmText = 'Confirmar';
  @Input() cancelText = 'Cancelar';

  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onCancel() {
    this.cancel.emit();
  }

  onConfirm() {
    this.confirm.emit();
  }
}
