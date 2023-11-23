// @ts-ignore
import {LoginInfo} from "./login-info";

export class AuthHelper{


  static setLoginInfo(x?: LoginInfo | null):void{
    if(x == null)
      x = new LoginInfo();
    localStorage.setItem("authentication-token", JSON.stringify(x));
  }

  static getLoginInfo() : LoginInfo {
    let x = localStorage.getItem("authentication-token");
    if(x==null || x==="")
      return new LoginInfo();

    try{
      let loginInformation: LoginInfo=JSON.parse(x);
      if(loginInformation==null)
        return new LoginInfo();
      return loginInformation;

    }
    catch (e){
      return new LoginInfo();
    }
  }
}
