// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import {AuthHelper} from "../app/helper/auth-helper";

export class environment {
  static production = false;
  static apiBaseUrl = 'https://localhost:7242/';

  static http_opcije= function (){

    let authenticationToken = AuthHelper.getLoginInfo().authenticationToken;
    let myToken = "";

    if (authenticationToken!=null)
      myToken = authenticationToken.tokenValue;
    console.log(myToken);
    return {
      headers: {
        'authentication-token': myToken,
      }
    };
  }
}


/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
