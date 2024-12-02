import { UserDTO } from './user_dto';

export interface UserTokenDTO {
  token: string;
  refreshToken: string;
  expiration: Date;
  user: UserDTO;
}
