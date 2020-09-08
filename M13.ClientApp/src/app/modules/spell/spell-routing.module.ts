import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SpellComponent } from './components/spell/spell.component';

const routes: Routes = [
  {
    path: '',
    component: SpellComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SpellRoutingModule {}