import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import {CommonModule} from '@angular/common';
import { RepositoryItem } from '@models/repository.model';

@Component({
	selector: 'repository-item',
	templateUrl: './repository-item.component.html',
	styleUrls: ['./repository-item.component.css'],
  imports:[CommonModule],
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RepositoryItemComponent{

    @Input()
    item: RepositoryItem;

    @Output()
    onBookmarked = new EventEmitter<RepositoryItem>();

    bookmark(): void {
        this.item.isMarked = !this.item.isMarked;
        this.onBookmarked.emit(this.item);
    }
}
