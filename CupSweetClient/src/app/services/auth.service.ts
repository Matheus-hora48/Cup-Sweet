import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { UserTokenDTO } from '../dto/user_token_dto';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://cup-sweet.onrender.com/api/auth/';
  constructor(private http: HttpClient, private toastr: ToastrService) {}

  login(
    email: string,
    password: string
  ): Observable<{ token: string; refreshToken: string; expiration: Date }> {
    return this.http
      .post<{
        token: string;
        refreshToken: string;
        expiration: Date;
      }>(`${this.baseUrl}login/`, { email, password })
      .pipe(
        catchError((error) => {
          this.toastr.error(
            error.error?.message || 'Erro ao fazer login',
            'Erro'
          );
          return throwError(() => error);
        })
      );
  }

  register(
    name: string,
    contato: string,
    email: string,
    password: string
  ): Observable<{ status: string; message: string }> {
    return this.http
      .post<{ status: string; message: string }>(`${this.baseUrl}register/`, {
        email,
        password,
        phoneNumber: contato,
        userName: name.split(' ')[0],
      })
      .pipe(
        catchError((error) => {
          this.toastr.error(
            error.error?.message || 'Erro ao registrar',
            'Erro'
          );
          return throwError(() => error);
        })
      );
  }

  recoverPassword(email: string): boolean {
    console.log('Recuperação de senha enviada para:', email);
    return true;
  }

  setToken(response: {
    token: string;
    refreshToken: string;
    expiration: Date;
  }) {
    let userToken: UserTokenDTO = {
      expiration: response.expiration,
      token: response.token,
      refreshToken: response.refreshToken,
      user: {
        email: this.getClaim(response.token, 'email')!,
        name: this.getClaim(response.token, 'unique_name')!,
        id: this.getClaim(response.token, 'id')!,
        phone: this.getClaim(response.token, 'phone')!,
        role: this.getClaim(response.token, 'role')!,
      },
    };

    localStorage.setItem(`userToken`, JSON.stringify(userToken));
  }

  private extractClaims(token: string) {
    try {
      const decoded: any = jwtDecode(token);
      return decoded;
    } catch (error) {
      return null;
    }
  }

  private getClaim(token: string, claimName: string): string | null {
    const decoded = this.extractClaims(token);
    if (decoded && decoded[claimName]) {
      return decoded[claimName];
    }
    return null;
  }

  getUser() {
    const userToken = localStorage.getItem('userToken');
    if (userToken) {
      const userTokenDTO: UserTokenDTO = JSON.parse(userToken);
      return userTokenDTO.user;
    }
    return null;
  }

  logout() {
    localStorage.removeItem('userToken');
  }

  isAuthenticated(): boolean {
    const userToken = localStorage.getItem('userToken');
    return userToken ? true : false;
  }
}
