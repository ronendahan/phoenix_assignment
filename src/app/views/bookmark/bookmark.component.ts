import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import {BookmarkService} from '@services/bookmark.service';
import { RepositoryItem } from '@models/repository.model';
import {RepositoryListComponent} from '@share/repository-list/repository-list.component';

@Component({
    selector: 'app-bookmark',
    standalone: true,
    imports: [
        CommonModule,
        RepositoryListComponent
    ],
    templateUrl: './bookmark.component.html',
    styleUrls: ['./bookmark.component.css'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class BookmarkComponent {

    bookmarkService = inject(BookmarkService);
    bookmarks$: Observable<RepositoryItem[]>;

    ngOnInit(): void {
        this.bookmarks$ = this.bookmarkService.getBookmarks();
    }
}
