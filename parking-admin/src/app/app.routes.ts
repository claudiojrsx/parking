import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  // 🔓 Login (público)
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login.component')
        .then(m => m.LoginComponent),
  },

  // 🔐 Área autenticada com layout
  {
    path: '',
    canActivate: [AuthGuard],
    loadComponent: () =>
      import('./features/layout/main-layout.components')
        .then(m => m.MainLayoutComponent),
    children: [
      // 👉 rota padrão após login
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard',
      },

      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard.component')
            .then(m => m.DashboardComponent),
      },

      {
        path: 'admin',
        data: { role: 'Admin' },
        loadComponent: () =>
          import('./features/admin/admin.component')
            .then(m => m.AdminComponent),
      },

      {
        path: 'parking-spots',
        loadComponent: () =>
          import('./features/parking-spots/parking-spots.component')
            .then(m => m.ParkingSpotsComponent),
      },
    ],
  },

  // ❌ qualquer rota inválida
  {
    path: '**',
    redirectTo: 'login',
  },
];
