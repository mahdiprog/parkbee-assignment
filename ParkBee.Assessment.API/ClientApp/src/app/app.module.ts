import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { GarageComponent } from './garage/garage.component';
import { GarageService } from './garage/garage.service';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AuthService } from './services/auth.service';
import { TokenStorageService } from './services/token-storage.service';
import { LocalStorage } from './shared/local-storage';
import { SessionStorage } from './shared/session-storage';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GarageComponent
  ],
  imports: [
    //CoreModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'garage', component: GarageComponent }
], { relativeLinkResolution: 'legacy' })
  ],
  providers: [
    AuthService, GarageService,
    TokenStorageService,
    { provide: LocalStorage, useFactory: () => localStorage },
    { provide: SessionStorage, useFactory: () => sessionStorage },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
