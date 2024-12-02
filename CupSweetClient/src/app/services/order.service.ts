import { Injectable } from '@angular/core';
import { PaymentMethod } from '../enuns/payment-method.enum';
import { Order } from '../dto/order_dto';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  getOrders(): Observable<any[]> {
    const id = this.authService.getUser()?.id;
    return this.http.get<any[]>(
      `https://localhost:7046/api/pedido/usuario/${id}`
    );
  }
}
