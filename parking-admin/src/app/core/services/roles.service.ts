import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateRoleRequest, Role } from '../models/role.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RolesService {
  private readonly apiUrl = 'https://localhost:7097/api/roles';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Role[]> {
    return this.http.get<Role[]>(this.apiUrl);
  }

  create(request: CreateRoleRequest) {
    return this.http.post<void>(this.apiUrl, request);
  }
}
