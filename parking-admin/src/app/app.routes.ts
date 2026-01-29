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
        data: { breadcrumb: 'Dashboard' },
        loadComponent: () =>
          import('./features/dashboard/dashboard.component').then(
            (m) => m.DashboardComponent,
          ),
      },
      {
        path: 'admin',
        data: { role: 'Admin', breadcrumb: 'Admin' },
        loadComponent: () =>
          import('./features/layout/router-outlet/admin/admin.component').then(
            (m) => m.AdminComponent,
          ),
        children: [
          {
            path: '',
            pathMatch: 'full',
            redirectTo: 'pricing',
          },
          {
            path: 'pricing',
            data: { breadcrumb: 'Pricing' },
            loadComponent: () =>
              import('./features/layout/router-outlet/admin/pricing/pricing.component').then(
                (m) => m.PricingComponent,
              ),
          },
          {
            path: 'users/create',
            data: { breadcrumb: 'Criar Usuário' },
            loadComponent: () =>
              import('./features/layout/router-outlet/admin/users/create-user/create-user.component').then(
                (m) => m.CreateUserComponent,
              ),
          },
          {
            path: 'users',
            data: { breadcrumb: 'Lista de Usuários' },
            loadComponent: () =>
              import('./features/layout/router-outlet/admin/users/list-users/list-users.component').then(
                (m) => m.ListUsersComponent,
              ),
          },
          {
            path: 'roles/create',
            data: { breadcrumb: 'Criar Role' },
            loadComponent: () =>
              import('./features/layout/router-outlet/admin/roles/create/create-role/create-role.component').then(
                (m) => m.CreateRoleComponent,
              ),
          },
        ],
      },
      {
        path: 'parking',
        data: { breadcrumb: 'Parking' },
        children: [
          {
            path: 'check-in',
            data: { breadcrumb: 'Check-in' },
            loadComponent: () =>
              import('./features/parking-check-in/parking-check-in.component').then(
                (m) => m.ParkingCheckInComponent,
              ),
          },
          {
            path: 'check-out',
            data: { breadcrumb: 'Check-out' },
            loadComponent: () =>
              import('./features/parking-check-out/parking-check-out.component').then(
                (m) => m.ParkingCheckOutComponent,
              ),
          },
        ],
      },
      {
        path: 'parking-spots',
        data: { breadcrumb: 'Vagas de Estacionamento' },
        loadComponent: () =>
          import('./features/parking-spots/parking-spots.component').then(
            (m) => m.ParkingSpotsComponent,
          ),
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];
