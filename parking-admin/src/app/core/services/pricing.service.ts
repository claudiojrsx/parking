// src/app/core/services/pricing.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PricingConfig } from '../models/pricing-config.model';

@Injectable({
  providedIn: 'root',
})
export class PricingService {
  private readonly apiUrl = 'https://localhost:7097/api/pricing';

  constructor(private http: HttpClient) {}

  /** Busca preço atual */
  getCurrent(): Observable<{
    motorcycle: number;
    car: number;
    truck: number;
  }> {
    return this.http.get<{
      motorcycle: number;
      car: number;
      truck: number;
    }>(`${this.apiUrl}/current`);
  }

  /** Salva preços */
  save(payload: PricingConfig): Observable<void> {
    return this.http.post<void>(this.apiUrl, payload);
  }
}
