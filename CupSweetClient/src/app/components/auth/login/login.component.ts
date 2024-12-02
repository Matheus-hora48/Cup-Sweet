import { Component } from '@angular/core';
import { LogoComponent } from '../../../shared/logo/logo.component';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [LogoComponent, FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  login(): void {
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        if (response) {
          this.authService.setToken(response);
          this.toastr.success('Login realizado com sucesso!', 'Sucesso');
          this.router.navigate(['/home']);
        } else {
          this.toastr.error('Credenciais inválidas!', 'Erro');
        }
      },
      error: () => {
        this.toastr.error('Erro ao realizar o login. Tente novamente.', 'Erro');
      },
    });
  }

  goToRegister(): void {
    this.router.navigate(['/register']);
  }

  goToForgotPassword(): void {
    this.router.navigate(['/forgot-password']);
  }
}
