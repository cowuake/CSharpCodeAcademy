import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewBillComponent } from './components/new-bill/new-bill.component';

const routes: Routes = [{ path: 'new-bill', component: NewBillComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [NewBillComponent];
