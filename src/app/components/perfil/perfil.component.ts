import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {
  usuario: any = null;
  tokenActual: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.tokenActual = this.authService.getToken();

    this.authService.getMe().subscribe({
      next: (data: any) => {
        this.usuario = {
          nombre: data?.nombre || data?.Nombre || data?.nombreCompleto || data?.NombreCompleto || 'Usuario',
          email: data?.email || data?.Email || '',
          roles: data?.roles || data?.Roles || ['Usuario']
        };
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(err);
        this.authService.clearToken();
        this.router.navigate(['/']);
      }
    });
  }

  obtenerRoles(): string {
    if (!this.usuario || !this.usuario.roles) return 'Usuario';
    if (Array.isArray(this.usuario.roles)) {
      return this.usuario.roles.join(', ');
    }
    return this.usuario.roles;
  }

  logout(): void {
    this.authService.clearToken();
    this.router.navigate(['/']);
  }
}
