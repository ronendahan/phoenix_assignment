import { Component, Input, ChangeDetectionStrategy, inject, OnDestroy } from '@angular/core';
import {CommonModule} from '@angular/common';
import { Observable, Subscription } from 'rxjs';
import { RepositoryItem } from '@models/repository.model';
import {BookmarkService} from '@services/bookmark.service';
import { RepositoryItemComponent} from '@app/share/repository-list/repository-item/repository-item.component';
import { SearchInputComponent } from '@app/share/search-input/search-input.component';
import { NoResultsComponent } from '@app/share/no-results/no-results.component';

@Component({
  selector: 'repository-list',
  templateUrl: './repository-list.component.html',
  styleUrls: ['./repository-list.component.css'],
  imports:[
      CommonModule,
      RepositoryItemComponent,
      SearchInputComponent,
      NoResultsComponent
  ],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RepositoryListComponent implements OnDestroy {
    @Input()
    repositories: RepositoryItem[]

    bookmarkService = inject(BookmarkService);
    private _bookmarkSubscribare$: Observable<number>;
    private _bookmarkSubscription: Subscription;

    trackBy(index: number, item: RepositoryItem): string {
        return item.id.toString();
    }

    handleBookmark(item: RepositoryItem) {
        if (item.isMarked) {
            this._bookmarkSubscribare$ = this.bookmarkService.addBookmark(item)
        }
        else {
            this._bookmarkSubscribare$ = this.bookmarkService.removeBookmark(item.id)
        }

        this._bookmarkSubscription = this._bookmarkSubscribare$.subscribe();
    }

    ngOnDestroy(): void {
        if (this._bookmarkSubscription) {
            this._bookmarkSubscription.unsubscribe();
        }
    }
}
