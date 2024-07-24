import { Component, Output, EventEmitter, ChangeDetectionStrategy, inject } from '@angular/core';
import { Router } from '@angular/router';
import {SearchService} from '@services/search.service';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavigatorComponent {

    searchService = inject(SearchService);
    router = inject(Router);

    @Output()
    onViewSelected = new EventEmitter;

    setView(path: string): void {
        this.onViewSelected.emit(path);
    }

    query(key: string) {
        this.searchService.query(key)
        this.router.navigate(['repositories']);
    }
}
