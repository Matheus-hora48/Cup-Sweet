import { Injectable } from '@angular/core';
import { Product } from '../dto/product_dto';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cartKey = 'cart';

  getCart() {
    const cart = localStorage.getItem(this.cartKey);
    return cart ? JSON.parse(cart) : [];
  }

  addProduto(product: Product) {
    let cart = this.getCart();

    if (!Array.isArray(cart)) {
      cart = [];
    }

    const productIndex = cart.findIndex(
      (p: Product) => p.produtoId === product.produtoId
    );

    if (productIndex === -1) {
      cart.push(product);
    } else {
      cart[productIndex].quantity += product.quantity;
    }

    localStorage.setItem(this.cartKey, JSON.stringify(cart));
  }

  removeProduto(productId: number) {
    let cart = this.getCart();

    if (Array.isArray(cart)) {
      cart = cart.filter((product: Product) => product.produtoId !== productId);
      localStorage.setItem(this.cartKey, JSON.stringify(cart));
    } else {
      console.error('Erro: carrinho não é um array');
    }
  }

  cleanCart() {
    localStorage.removeItem(this.cartKey);
  }

  updateQtd(productId: number, qtd: number) {
    let cart = this.getCart();

    if (Array.isArray(cart)) {
      const productIndex = cart.findIndex(
        (product: Product) => product.produtoId === productId
      );

      if (productIndex > -1) {
        cart[productIndex].quantity = qtd;
        localStorage.setItem(this.cartKey, JSON.stringify(cart));
      }
    } else {
      console.error('Erro: carrinho não é um array');
    }
  }

  getTotalQuantity(): number {
    const cart = this.getCart();
    if (Array.isArray(cart)) {
      return cart.reduce(
        (total: number, product: Product) => total + product.quantity!,
        0
      );
    }
    return 0;
  }
}
