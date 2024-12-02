import { HttpClient, HttpInterceptorFn } from '@angular/common/http';
import { UserTokenDTO } from '../dto/user_token_dto';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const userToken = localStorage.getItem('userToken');

  if (!userToken) {
    return next(req);
  }

  let userTokenDTO: UserTokenDTO = JSON.parse(userToken);
  let http = inject(HttpClient);
  let authService = inject(AuthService);
  let router = inject(Router);

  if (userTokenDTO.expiration > new Date()) {
    http
      .post('https://localhost:7046/api/auth/refresh-token/', {
        accessToken: userTokenDTO.token,
        refreshToken: userTokenDTO.refreshToken,
      })
      .subscribe(
        (response: any) => {
          authService.setToken(response);
        },
        (error) => {
          console.error('Erro ao atualizar o token:', error);
          localStorage.removeItem('userToken');
          router.navigate(['/login']);
          return next(req);
        }
      );
  }

  userTokenDTO = JSON.parse(localStorage.getItem('userToken')!);

  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${userTokenDTO.token}`,
    },
  });

  return next(authReq);
};
