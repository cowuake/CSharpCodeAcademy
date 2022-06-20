import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SillyButtonComponent } from './components/silly-button/silly-button.component';
import { GifContainerComponent } from './components/gif-container/gif-container.component';
import { CowsComponent } from './components/cows/cows.component';
import { CowItemComponent } from './components/cow-item/cow-item.component';
import { AddCowComponent } from './components/add-cow/add-cow.component';
import { AddButtonComponent } from './components/add-button/add-button.component';
import { FilterCowsPipe } from './filter-cows.pipe';
import { FilterCowsComponent } from './filter-cows/filter-cows.component';
import { CowGameComponent } from './components/cow-game/cow-game.component';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents, // Pay attention to this!
    HeaderComponent,
    SillyButtonComponent,
    GifContainerComponent,
    CowsComponent,
    CowItemComponent,
    AddCowComponent,
    AddButtonComponent,
    FilterCowsPipe,
    FilterCowsComponent,
    CowGameComponent,
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
