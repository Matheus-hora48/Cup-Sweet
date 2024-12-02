import { Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { Product } from '../dto/product_dto';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  private selectedAddress: any = null;
  private selectedPaymentMethod: string = 'Pix';

  private mockPaymentMethods = [
    { id: 2, name: 'Pix' },
    { id: 3, name: 'Cartão de Crédito' },
    { id: 4, name: 'Boleto Bancário' },
  ];

  constructor(
    private cartService: CartService,
    private http: HttpClient,
    private authService: AuthService
  ) {}

  setSelectedAddress(address: any) {
    this.selectedAddress = address;
  }

  getSelectedAddress() {
    return this.selectedAddress;
  }

  getCartItems() {
    return this.cartService.getCart();
  }

  getTotalPrice(): number {
    const cartItems = this.getCartItems();
    return cartItems.reduce((acc: number, item: any) => {
      return acc + item.preco * item.quantity;
    }, 0);
  }

  setSelectedPaymentMethod(method: string) {
    this.selectedPaymentMethod = method;
  }

  getSelectedPaymentMethod() {
    return this.selectedPaymentMethod;
  }

  getPaymentMethods() {
    return this.mockPaymentMethods;
  }

  completeCheckout() {
    const userId = this.authService.getUser()?.id;
    const cartItems = this.getCartItems();
    const address = this.getSelectedAddress();

    const order = {
      userId,
      enderecoId: address.enderecoId,
      pagamentos: [
        {
          dataPagamento: new Date(),
          formaPagamento: 2,
          valor: this.getTotalPrice(),
        },
      ],
      produtos: cartItems.map((item: Product) => ({
        produtoId: item.produtoId,
        quantidade: item.quantity,
      })),
    };

    return this.http.post('https://localhost:7046/api/Pedido', order);
  }
}
