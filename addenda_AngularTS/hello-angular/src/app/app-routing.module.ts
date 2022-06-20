import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CowsComponent } from './components/cows/cows.component';
import { GifContainerComponent } from './components/gif-container/gif-container.component';

const routes: Routes = [
  { path: 'home', component: GifContainerComponent },
  { path: 'cows', component: CowsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [GifContainerComponent, CowsComponent];
