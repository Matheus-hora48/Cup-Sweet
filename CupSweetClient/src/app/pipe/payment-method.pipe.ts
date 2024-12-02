import { Pipe, PipeTransform } from '@angular/core';
import { PaymentMethod } from '../enuns/payment-method.enum';

@Pipe({
  name: 'paymentMethod',
  standalone: true,
})
export class PaymentMethodPipe implements PipeTransform {
  transform(value: PaymentMethod, ...args: unknown[]): string {
    {
      switch (value) {
        case PaymentMethod.CartaoCredito:
          return 'Cartão de Crédito';
        case PaymentMethod.CartaoDebito:
          return 'Cartão de Débito';
        case PaymentMethod.Pix:
          return 'PIX';
        case PaymentMethod.Dinheiro:
          return 'Dinheiro';
        default:
          return 'Indefinido';
      }
    }
  }
}
