import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly TOKEN_KEY = 'parking_token';
  private isBrowser: boolean;

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  login(email: string, password: string) {
    return this.http
      .post<any>('https://localhost:7097/api/auth/login', {
        email,
        password
      })
      .pipe(
        tap(response => {
          if (this.isBrowser && response?.token) {
            localStorage.setItem(this.TOKEN_KEY, response.token);
          }
        })
      );
  }

  logout(): void {
    if (this.isBrowser) {
      localStorage.removeItem(this.TOKEN_KEY);
    }
  }

  get token(): string | null {
    if (!this.isBrowser) return null;
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  getClaims(): any {
    if (!this.token) return null;

    try {
      return JSON.parse(atob(this.token.split('.')[1]));
    } catch {
      return null;
    }
  }

  hasRole(role: string): boolean {
    const claims = this.getClaims();
    return claims?.role === role || claims?.roles?.includes(role);
  }
}
