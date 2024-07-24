import {inject, Injectable} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, throwError} from 'rxjs';
import {map, catchError, switchMap } from 'rxjs/operators';
import {Repository} from '@models/repository.model';
import {environment} from '@environments/environment';

@Injectable({
    providedIn: 'root'
})
export class RepositoryService {
    http = inject(HttpClient);

    getRepositoriesByKey(key: string) : Observable<Repository> {
        return this.http.get(environment.services.repositories + '?q=' + key)
        .pipe(
            map((res: any) => {
                return {...res.repository}
            }),
            catchError(err => {
                return throwError(() => new Error(err));
            })
        );
    }
}
