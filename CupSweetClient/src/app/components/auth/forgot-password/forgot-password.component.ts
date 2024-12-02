import { Component } from '@angular/core';
import { LogoComponent } from '../../../shared/logo/logo.component';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [LogoComponent, FormsModule, HttpClientModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  email: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  recoverPassword(): void {
    const success = this.authService.recoverPassword(this.email);
    if (success) {
      alert('Email de recuperação enviado!');
      this.router.navigate(['/login']);
    } else {
      alert('Erro ao enviar email de recuperação.');
    }
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}
