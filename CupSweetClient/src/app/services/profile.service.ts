import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  getUserProfile(): Observable<any> {
    const id = this.authService.getUser()?.id;
    return this.http.get(`https://localhost:7046/api/Auth/${id}`);
  }
}
