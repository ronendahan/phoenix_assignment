import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RepositoryComponent } from '@app/views/repository/repository.component';
import {BookmarkComponent} from '@app/views/bookmark/bookmark.component';

const routes: Routes = [
    {path: 'repositories', loadComponent: () => import('@app/views/repository/repository.component').then(c => c.RepositoryComponent)},
    {path: 'bookmarks', loadComponent: () => import('@app/views/bookmark/bookmark.component').then(c => c.BookmarkComponent)},
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
