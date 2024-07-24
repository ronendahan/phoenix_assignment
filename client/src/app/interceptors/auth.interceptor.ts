import { Injectable, inject } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import {AuthService} from '@services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    authService = inject(AuthService);
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let request = req;
        if (this.authService.token) {
            // add Authorization header to the request
            request = req.clone({
                setHeaders: { Authorization: `Bearer ${this.authService.token}` }
            });
        }
        return next.handle(request);
    }
}
