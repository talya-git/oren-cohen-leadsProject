import { Routes } from '@angular/router';
import { authGuard, managerGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent) },
  { path: 'manager', loadComponent: () => import('./pages/manager-dashboard/manager-dashboard.component').then(m => m.ManagerDashboardComponent), canActivate: [managerGuard] },
  { path: 'agent', loadComponent: () => import('./pages/agent-dashboard/agent-dashboard.component').then(m => m.AgentDashboardComponent), canActivate: [authGuard] },
  { path: '**', redirectTo: '/login' }
];
