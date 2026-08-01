import { Routes } from '@angular/router';
import { PrincipalComponent } from './components/principal/principal.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { authGuard } from './seguridad/auth.guard';

export const routes: Routes = [
  { path: 'inicio', component: PrincipalComponent },
  {
    path: 'perfil',
    component: PerfilComponent,
    canActivate: [authGuard]
  },
   { path: 'Usuario', component: PerfilComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'inicio' }
];

