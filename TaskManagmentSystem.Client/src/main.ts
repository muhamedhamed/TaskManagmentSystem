import { bootstrapApplication, provideProtractorTestingSupport } from '@angular/platform-browser';
import { AppComponent } from './app/components/app/app.component';
import { provideRouter } from '@angular/router';
import routeConfig from './app/app.routes';
import { provideHttpClient, withFetch } from '@angular/common/http';

  bootstrapApplication(AppComponent,
    {
      providers: [
        provideProtractorTestingSupport(),
        provideHttpClient(withFetch()),
        provideRouter(routeConfig)
      ]
    }
  ).catch(err => console.error(err));
