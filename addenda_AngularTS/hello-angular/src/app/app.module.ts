import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SillyButtonComponent } from './components/silly-button/silly-button.component';
import { GifContainerComponent } from './components/gif-container/gif-container.component';
import { CowsComponent } from './components/cows/cows.component';
import { CowItemComponent } from './components/cow-item/cow-item.component';
import { AddCowComponent } from './components/add-cow/add-cow.component';
import { AddButtonComponent } from './components/add-button/add-button.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SillyButtonComponent,
    GifContainerComponent,
    CowsComponent,
    CowItemComponent,
    AddCowComponent,
    AddButtonComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
