import { Review } from './review_dto';

export interface Product {
  produtoId: number;
  nome: string;
  descricao: string;
  preco: number;
  imageUrl: string;
  avaliacoes: Review[];
  estoque: number;
  quantity?: number;
}
