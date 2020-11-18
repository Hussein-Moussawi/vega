import { ErrorHandler, Injectable, Injector, NgZone } from "@angular/core";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class AppErrorHandler implements ErrorHandler {

  private toastrService: ToastrService;

  constructor(private injector: Injector, private ngZone : NgZone) {}

  handleError(error: any): void {

    this.ngZone.run(() => {
      this.toastrService = this.injector.get(ToastrService);
      this.toastrService.error('everything is broken', 'Major Error', {
        timeOut: 3000,
        positionClass: 'md-toast-top-right',
      });
    })

    
    }
}
