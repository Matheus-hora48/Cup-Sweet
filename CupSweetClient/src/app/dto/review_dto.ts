import { UserDTO } from './user_dto';

export interface Review {
  comentario: string;
  nota: number;
  data: Date;
  user: any;
}
