import {RegisteredUser} from "../models/registered-user";

export class LoginInfo{
  authenticationToken?: AutheticationToken|null=null;
  isLogged:boolean=false;
}

export interface AutheticationToken{
  id: number;
  tokenValue: string;
  registeredUserId:number;
  registeredUser: RegisteredUser;
  timeOfGeneration: Date;
  isAddress:string;
}
