import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { PricingService } from '../../../core/services/pricing.service';

@Component({
  standalone: true,
  selector: 'app-pricing',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './pricing.component.html',
})
export class PricingComponent implements OnInit {
  loading = false;
  success = false;
  error = '';

  form = this.fb.group({
    motorcycle: [0, [Validators.required, Validators.min(0.01)]],
    car: [0, [Validators.required, Validators.min(0.01)]],
    truck: [0, [Validators.required, Validators.min(0.01)]],
  });

  constructor(
    private fb: FormBuilder,
    private pricingService: PricingService,
  ) {}

  ngOnInit(): void {
    this.loadCurrentPricing();

    // reseta feedback ao editar
    this.form.valueChanges.subscribe(() => {
      this.success = false;
      this.error = '';
    });
  }

  loadCurrentPricing() {
    this.loading = true;

    this.pricingService.getCurrent().subscribe({
      next: (data) => {
        this.form.patchValue(data);
      },
      error: () => {
        this.error = 'Erro ao carregar preços atuais';
      },
      complete: () => (this.loading = false),
    });
  }

  save() {
    if (this.form.invalid) return;

    this.loading = true;
    this.success = false;
    this.error = '';

    this.pricingService.create(this.form.value).subscribe({
      next: () => {
        this.success = true;
      },
      error: () => {
        this.error = 'Erro ao salvar preços';
      },
      complete: () => (this.loading = false),
    });
  }
}
