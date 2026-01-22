import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CheckInRequest } from '../dtos/check-in-request';
import { CheckInResponse } from '../dtos/check-in-response';

@Injectable({ providedIn: 'root' })
export class ParkingService {
  private readonly apiUrl = 'https://localhost:7097/api/parking';

  constructor(private http: HttpClient) {}

  checkIn(request: CheckInRequest): Observable<CheckInResponse> {
    return this.http.post<CheckInResponse>(`${this.apiUrl}/check-in`, request);
  }
}
