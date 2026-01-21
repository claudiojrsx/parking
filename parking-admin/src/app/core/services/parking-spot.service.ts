import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ParkingSpotType } from '../enums/parking-spot-type.enum';

@Injectable({ providedIn: 'root' })
export class ParkingSpotService {
  private baseUrl = 'https://localhost:7097/api/parking-spots';

  constructor(private http: HttpClient) {}

  getAllByType(type: ParkingSpotType) {
    const params = new HttpParams().set('type', ParkingSpotType[type]);
    return this.http.get<any[]>(`${this.baseUrl}`, { params });
  }

  getAllAvailable(type: ParkingSpotType) {
    const params = new HttpParams().set('type', ParkingSpotType[type]);
    return this.http.get<any[]>(`${this.baseUrl}/all-available`, { params });
  }

  getOneAvailable(type: ParkingSpotType) {
    const params = new HttpParams().set('type', ParkingSpotType[type]);
    return this.http.get<any>(`${this.baseUrl}/available`, { params });
  }

  create(spot: { code: string; type: ParkingSpotType }) {
    return this.http.post(this.baseUrl, spot);
  }
}
