"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppErrorHandler = /** @class */ (function () {
    function AppErrorHandler(toastrService) {
        this.toastrService = toastrService;
    }
    AppErrorHandler.prototype.handleError = function (error) {
        this.toastrService.error('everything is broken', 'Major Error', {
            timeOut: 3000,
            positionClass: 'md-toast-top-right',
        });
    };
    return AppErrorHandler;
}());
exports.AppErrorHandler = AppErrorHandler;
//# sourceMappingURL=app.error-handler.js.map