import {inject, Injectable} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, throwError} from 'rxjs';
import {map, catchError, switchMap } from 'rxjs/operators';
import {RepositoryItem} from '@models/repository.model';
import {environment} from '@environments/environment';

@Injectable({
    providedIn: 'root'
})
export class BookmarkService {
    http = inject(HttpClient);

    getBookmarks() : Observable<RepositoryItem[]> {
    return this.http.get(environment.services.bookmarks)
        .pipe(
            map((res: any) => {
                return [...res.bookmarks]
            }),
            catchError(err => {
                return throwError(() => new Error(err));
            })
        );
    }

    addBookmark(bookmark: RepositoryItem) : Observable<number> {
      return this.http.post(environment.services.bookmarks, bookmark)
          .pipe(
              map((res: any) => {
                  return res.bookmark
              }),
              catchError(err => {
                  return throwError(() => new Error(err));
              })
          );
      }

      removeBookmark(bookmarkId: number) : Observable<number> {
        return this.http.delete(environment.services.bookmarks + '/' + bookmarkId)
            .pipe(
                map((res: any) => {
                    return res.bookmark
                }),
                catchError(err => {
                    return throwError(() => new Error(err));
                })
            );
      }
}
