import { Component } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { Product } from '../../dto/product_dto';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    NavbarComponent,
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
})
export class CartComponent {
  cartItems: Product[] = [];
  totalAmount: number = 0;

  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    this.cartItems = this.cartService.getCart();
    this.calculateTotal();
  }

  calculateTotal() {
    this.totalAmount = this.cartItems.reduce((total, product) => {
      if (product.quantity === undefined) {
        product.quantity = 1;
      }
      return total + product.preco * product.quantity;
    }, 0);
  }

  updateQuantity(productId: number, quantity: number) {
    if (quantity > 0) {
      this.cartService.updateQtd(productId, quantity);
      this.loadCart();
    }
  }

  removeProduct(productId: number) {
    this.cartService.removeProduto(productId);
    this.loadCart();
  }

  checkout() {
    this.router.navigate(['/checkout']);
  }
}
