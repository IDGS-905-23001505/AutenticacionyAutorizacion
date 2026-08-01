import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-principal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './principal.component.html',
  styleUrls: ['./principal.component.css']
})
export class PrincipalComponent {
  mostrarModalRegistro = false;
  mostrarModalLogin = false;

  datosRegistro = { nombreCompleto: '', email: '', password: '' };
  datosLogin = { email: '', password: '' };

  constructor(private authService: AuthService, private router: Router) {}

  abrirRegistro() { this.mostrarModalRegistro = true; }
  cerrarRegistro() { this.mostrarModalRegistro = false; }

  abrirLogin() { this.mostrarModalLogin = true; }
  cerrarLogin() { this.mostrarModalLogin = false; }

  enviarRegistro() {
    this.authService.register(this.datosRegistro).subscribe({
      next: () => {
        alert('¡Usuario registrado con éxito!');
        this.datosRegistro = { nombreCompleto: '', email: '', password: '' };
        this.cerrarRegistro();
      },
      error: () => alert('Error en el registro. Revisa tus datos.')
    });
  }

  enviarLogin() {
    this.authService.login(this.datosLogin).subscribe({
      next: (res) => {
        this.authService.saveToken(res.token || res.tokenJwt || res.result);
        this.cerrarLogin();
        this.router.navigate(['/Usuario']);
      },
      error: () => alert('Credenciales incorrectas o error en el servidor.')
    });
  }
}
