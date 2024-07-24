import {inject, Injectable} from '@angular/core';
import { Repository } from '@models/repository.model';
import {RepositoryService} from '@services/repository.service';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import {auditTime, switchMap, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
    repositoryService = inject(RepositoryService);
    private _search$ = new BehaviorSubject<string>('');
    onSearch$ = this._search$.asObservable();

    query(key: string): void {
        this._search$.next(key)
    }
}
