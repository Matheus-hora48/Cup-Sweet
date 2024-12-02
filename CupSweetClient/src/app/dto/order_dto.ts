import { Address } from "./address_dto";
import { Payment } from "./payment_dto";
import { Product } from "./product_dto";

export interface Order {
    pedidoId: number;
    dataPedido: string;
    produtos: Product[];
    valorTotal: number;
    enderecoId: number;
    endereco: Address;
    pagamentos: Payment[];
    userId: string;
  }
  