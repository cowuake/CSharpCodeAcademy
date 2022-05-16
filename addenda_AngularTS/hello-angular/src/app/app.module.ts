import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SillyButtonComponent } from './components/silly-button/silly-button.component';
import { GifContainerComponent } from './components/gif-container/gif-container.component';
import { CowsComponent } from './components/cows/cows.component';
import { CowItemComponent } from './components/cow-item/cow-item.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SillyButtonComponent,
    GifContainerComponent,
    CowsComponent,
    CowItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
