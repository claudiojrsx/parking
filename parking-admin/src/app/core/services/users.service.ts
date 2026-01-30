import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateUserRequest, User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UsersService {
  private readonly apiUrl = 'https://localhost:7097/api/users';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<User[]>(this.apiUrl);
  }

  create(request: CreateUserRequest): Observable<void> {
    return this.http.post<void>(this.apiUrl, request);
  }

  getById(id: string) {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  update(id: string, data: any) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }
}
