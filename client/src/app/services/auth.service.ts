import {inject, Injectable} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {throwError} from 'rxjs';
import {tap, catchError } from 'rxjs/operators';
import {environment} from '@environments/environment'

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    http = inject(HttpClient);
    private _token: string;

    authenticate() : void {
        this.http.post(environment.services.auth, null)
        .pipe(
            tap((res: any) => {
                this._token = res.token;
            }),
            catchError(err => {
                return throwError(() => new Error(err));
            })
        ).subscribe();
    }

    get token() {
        return this._token;
    }
}
