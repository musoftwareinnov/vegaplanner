import { ErrorHandler } from "../../node_modules/@angular/core";

export class AppErrorHandler implements ErrorHandler {
    handleError(error: any):void {
        console.log("ERROR");
    }
}