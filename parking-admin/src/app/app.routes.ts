import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login.component').then(
        (m) => m.LoginComponent,
      ),
  },
  {
    path: '',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/layout/main-layout.components').then(
        (m) => m.MainLayoutComponent,
      ),
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard',
      },

      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard.component').then(
            (m) => m.DashboardComponent,
          ),
      },

      {
        path: 'admin',
        data: { role: 'Admin' },
        loadComponent: () =>
          import('./features/admin/admin.component').then(
            (m) => m.AdminComponent,
          ),
      },
      {
        path: 'parking-spots',
        loadComponent: () =>
          import('./features/parking-spots/parking-spots.component').then(
            (m) => m.ParkingSpotsComponent,
          ),
      },
      {
        path: 'parking/check-in',
        loadComponent: () =>
          import('./features/parking-check-in/parking-check-in.component').then(
            (m) => m.ParkingCheckInComponent,
          ),
      },
      {
        path: 'parking/check-out',
        loadComponent: () =>
          import('./features/parking-check-out/parking-check-out.component').then(
            (m) => m.ParkingCheckOutComponent,
          ),
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];
