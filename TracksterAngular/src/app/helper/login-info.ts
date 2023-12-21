import {RegisteredUser} from "../models/registered-user";

export class LoginInfo{
  authenticationToken?: AuthenticationToken|null=null;
  isLogged:boolean=false;
}

export interface AuthenticationToken{
  id: number;
  tokenValue: string;
  registeredUserId:number;
  registeredUser: RegisteredUser;
  timeOfGeneration: Date;
  isAddress:string;
}
