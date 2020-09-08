import { NgModule } from '@angular/core';

import { SpellComponent } from './components/spell/spell.component';
import { SharedModule } from '../../shared';
import { SpellRoutingModule } from './spell-routing.module';

@NgModule({
  imports: [
    SharedModule,
    SpellRoutingModule
  ],
  declarations: [
    SpellComponent
  ]
})
export class SpellModule {}