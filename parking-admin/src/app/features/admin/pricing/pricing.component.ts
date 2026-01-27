import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { PricingService } from '../../../core/services/pricing.service';
import { Pricing, VehicleType } from '../../../core/models/pricing.model';

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
export class PricingComponent implements OnInit, OnDestroy {
  loading = false;
  success = false;
  error = '';

  form!: FormGroup;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private pricingService: PricingService,
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      motorcycle: [0, [Validators.required, Validators.min(0.01)]],
      car: [0, [Validators.required, Validators.min(0.01)]],
      truck: [0, [Validators.required, Validators.min(0.01)]],
    });

    this.loadCurrentPricing();

    this.form.valueChanges.pipe(takeUntil(this.destroy$)).subscribe(() => {
      this.success = false;
      this.error = '';
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadCurrentPricing(): void {
    this.loading = true;

    this.pricingService.getCurrent().subscribe({
      next: (data) => {
        this.form.patchValue({
          motorcycle: data.motorcycle,
          car: data.car,
          truck: data.truck,
        });
      },
      error: (err) => {
        console.error(err);
        this.error = 'Erro ao carregar preços atuais';
        this.loading = false;
      },
      complete: () => (this.loading = false),
    });
  }

  save(): void {
    if (this.form.invalid) return;

    this.loading = true;
    this.success = false;
    this.error = '';

    const payload = this.form.getRawValue();

    this.pricingService.save(payload).subscribe({
      next: () => {
        this.success = true;
      },
      error: (err) => {
        console.error(err);
        this.error = 'Erro ao salvar preços';
        this.loading = false;
      },
      complete: () => (this.loading = false),
    });
  }

  // ---------- helpers ----------

  private getPrice(prices: Pricing[], type: VehicleType): number {
    return prices.find((p) => p.vehicleType === type)?.pricePerHour ?? 0;
  }

  private buildPricing(type: VehicleType, value: number): Pricing {
    return {
      vehicleType: type,
      pricePerHour: value,
      pricePerDay: 0,
      toleranceMinutes: 0,
      active: true,
    };
  }
}
