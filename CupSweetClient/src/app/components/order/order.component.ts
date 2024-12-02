import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { PaymentMethodPipe } from '../../pipe/payment-method.pipe';
import { OrderService } from '../../services/order.service';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, PaymentMethodPipe, HttpClientModule, NavbarComponent],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss',
})
export class OrderComponent {
  orders: any[] = [];
  isOpen: boolean[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderService.getOrders().subscribe((response) => {
      this.orders = response;
      this.isOpen = this.orders.map(() => false);
    });
  }

  toggleAccordion(index: number): void {
    const accordion = document.getElementById(`accordion-${index}`);
    this.isOpen[index] = !this.isOpen[index];
    if (accordion) {
      if (accordion.style.display === 'none') {
        accordion.style.display = 'block';
      } else {
        accordion.style.display = 'none';
      }
    }
  }
}
