// src/app/core/services/pricing.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Pricing } from '../models/pricing-config.model';

@Injectable({
  providedIn: 'root',
})
export class PricingService {
  private readonly apiUrl = '/api/pricing';

  constructor(private http: HttpClient) {}

  /** Lista todos os preços */
  getAll(): Observable<Pricing[]> {
    return this.http.get<Pricing[]>(this.apiUrl);
  }

  /** Busca preço atual (ex: ativo por veículo) */
  getCurrent(): Observable<Pricing[]> {
    return this.http.get<Pricing[]>(`${this.apiUrl}/current`);
  }

  /** Cria ou atualiza preços */
  save(pricing: Pricing[]): Observable<void> {
    return this.http.post<void>(this.apiUrl, pricing);
  }

  /** Ativa / desativa um preço */
  toggleActive(id: number, active: boolean): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/active`, { active });
  }
}
