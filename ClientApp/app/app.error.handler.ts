import { ErrorHandler, Injectable } from "@angular/core";
import { Router } from '@angular/router';
import {UNAUTHORIZED, BAD_REQUEST, FORBIDDEN} from "http-status-codes";

@Injectable()
export class AppErrorHandler implements ErrorHandler {

    static readonly REFRESH_PAGE_ON_TOAST_CLICK_MESSAGE: string = "An error occurred: Please click this message to refresh";
    static readonly DEFAULT_ERROR_TITLE: string = "Something went wrong";
 
    constructor(){};

    handleError(error: any):void {
        console.log("ERROR");

        let httpErrorCode = error.httpErrorCode;
        switch (httpErrorCode) {
            case UNAUTHORIZED:
                //this.router.navigateByUrl("/login");
                break;
            case FORBIDDEN:
                //this.router.navigateByUrl("/unauthorized");
                break;
            case BAD_REQUEST:
               this.showError(error.message);
                break;
            default:
               this.showError(error.message);
          }

    
    }

    private showError(message:string){
        console.error(message);
        // this.toastManager.error(message, DEFAULT_ERROR_TITLE, { dismiss: 'controlled'}).then((toast:Toast)=>{
        //         let currentToastId:number = toast.id;
        //         this.toastManager.onClickToast().subscribe(clickedToast => {
        //             if (clickedToast.id === currentToastId) {
        //                 this.toastManager.dismissToast(toast);
        //                 window.location.reload();
        //             }
        //         });
        //     });
      }
}