import { PaymentMethod } from '../enuns/payment-method.enum';

export interface Payment {
  pagamentoId: number;
  dataPagamento: string;
  formaPagamento: PaymentMethod;
  valor: number;
  pedidoId: number;
}
