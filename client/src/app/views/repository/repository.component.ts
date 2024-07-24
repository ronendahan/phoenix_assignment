import { Component, inject, OnInit , ChangeDetectorRef, ChangeDetectionStrategy} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { Repository, RepositoryItem } from '@models/repository.model';
import {RepositoryService} from '@services/repository.service';
import {SearchService} from '@services/search.service';
import {RepositoryListComponent} from '@share/repository-list/repository-list.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-repository',
  standalone: true,
  imports: [
    CommonModule,
    RepositoryListComponent
  ],
  templateUrl: './repository.component.html',
  styleUrls: ['./repository.component.css'],
  changeDetection:ChangeDetectionStrategy.OnPush

})
export class RepositoryComponent implements OnInit {
    repositoryService = inject(RepositoryService);
    activatedRoute = inject(ActivatedRoute)
    cdr = inject(ChangeDetectorRef)
    repository$: Observable<Repository>;

    constructor(private searchService: SearchService) {

    }

    findRepositories(key: string): void {
        this.repository$ = this.repositoryService.getRepositoriesByKey(key)
    }

    trackBy(index: number, item: RepositoryItem): string {
        return item.id.toString();
    }

    ngOnInit(): void {
        this.searchService.onSearch$.subscribe(key => {
            if (key.length > 0){
                this.cdr.markForCheck()
                this.findRepositories(key)
            }
        });
    }
}
