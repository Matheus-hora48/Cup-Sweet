import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LogoComponent } from '../../../shared/logo/logo.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [LogoComponent, FormsModule, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  name: string = '';
  contato: string = '';
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  register(): void {
    const result = this.authService
      .register(this.name, this.contato, this.email, this.password)
      .subscribe((result) => {
        if (result.status === 'Success') {
          alert('Usuário registrado com sucesso!');
          this.router.navigate(['/login']);
        } else {
          alert(`${result.message}`);
        }
      });
  }

  goToLogin(): void {
    this.router.navigate(['/login']);
  }
}
