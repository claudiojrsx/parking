import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  private isBrowser: boolean;

  constructor(
    private auth: AuthService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const role = route.data['role'];

    // Durante SSR, não bloqueia nem redireciona
    if (!this.isBrowser) {
      return true;
    }

    if (!this.auth.isAuthenticated()) {
      this.router.navigate(['/login']);
      return false;
    }

    if (role && !this.auth.hasRole(role)) {
      this.router.navigate(['/dashboard']);
      return false;
    }

    return true;
  }
}
