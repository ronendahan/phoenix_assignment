import { ChangeDetectionStrategy, Component, Output, EventEmitter} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'search-input',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './search-input.component.html',
  styleUrls: ['./search-input.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchInputComponent {

    searchKey: string;

    @Output()
    onSearchInput = new EventEmitter<string>();

    search(): void {
        this.onSearchInput.emit(this.searchKey);
    }
}
