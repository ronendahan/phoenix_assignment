import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
    selector: 'no-results',
    templateUrl: './no-results.component.html',
    styleUrls: ['./no-results.component.css'],
    imports:[CommonModule],
    standalone: true,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class NoResultsComponent {
    @Input()
    results: any = [];
}
