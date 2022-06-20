import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CowGameComponent } from './components/cow-game/cow-game.component';
import { CowsComponent } from './components/cows/cows.component';
import { GifContainerComponent } from './components/gif-container/gif-container.component';

const routes: Routes = [
  { path: 'home', component: GifContainerComponent },
  { path: 'cows', component: CowsComponent },
  { path: 'game', component: CowGameComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [
  GifContainerComponent,
  CowsComponent,
  CowGameComponent,
];
