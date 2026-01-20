import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly TOKEN_KEY = 'parking_token';

  constructor(private http: HttpClient) {}

  login(email: string, password: string) {
    return this.http.post<any>('https://api.seuservico.com/auth/login', {
      email,
      password
    }).pipe(
      tap(response => {
        localStorage.setItem(this.TOKEN_KEY, response.token);
      })
    );
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  get token(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  getClaims(): any {
    if (!this.token) return null;
    return JSON.parse(atob(this.token.split('.')[1]));
  }

  hasRole(role: string): boolean {
    const claims = this.getClaims();
    return claims?.role === role || claims?.roles?.includes(role);
  }
}
