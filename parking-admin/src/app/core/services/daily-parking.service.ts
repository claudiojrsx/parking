import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { DailyActiveParkingDto } from '../dtos/daily-parking.dto';
import { CheckInRequest } from '../dtos/check-in-request';
import { VehicleType } from '../enums/vehicle-type.enum';

@Injectable({ providedIn: 'root' })
export class DailyParkingService {

  private readonly baseUrl = 'https://localhost:7097/api/parking/daily';

  constructor(private http: HttpClient) {}

  registerEntry(vehicleId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/entry/${vehicleId}`, {});
  }

  registerEntryByPlate(plate: string, vehicleType: VehicleType): Observable<void> {
    const request: CheckInRequest = { plate, vehicleType };
    return this.http.post<void>(`${this.baseUrl}/entry`, request);
  }

  registerExit(usageId: string): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/exit/${usageId}`, {});
  }

  getActive(): Observable<DailyActiveParkingDto[]> {
    return this.http.get<DailyActiveParkingDto[]>(`${this.baseUrl}/active`);
  }
}
