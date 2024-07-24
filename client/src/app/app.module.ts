import { APP_INITIALIZER, NgModule, inject} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {CommonModule} from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import {AuthService} from '@services/auth.service';
import { AppComponent } from './app.component';
import { HeaderComponent } from "./header/header.component";
import { NavigatorComponent} from './header/navigator/navigator.component';
import {SearchInputComponent} from '@share/search-input/search-input.component';
import {AuthInterceptor} from '@interceptors/auth.interceptor';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    SearchInputComponent
],
  declarations: [
    AppComponent,
    HeaderComponent,
    NavigatorComponent,

  ],
  providers: [
      {
          provide: APP_INITIALIZER,
          useFactory: () => {
              const authService = inject(AuthService);
              authService.authenticate();
          }
      },
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
