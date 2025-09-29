import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MarkdownModule } from 'ngx-markdown';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import mermaid from 'mermaid';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    importProvidersFrom(
      HttpClientModule,
      MarkdownModule.forRoot({ loader: HttpClient })
    )
  ]
}).catch(err => console.error(err));

mermaid.initialize({
  startOnLoad: true,
  theme: 'default'
});