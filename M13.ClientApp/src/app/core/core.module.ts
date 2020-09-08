import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ApiService } from './services';
import { RuleService } from './services';
import { SpellService } from './services';

@NgModule({
  imports: [
    CommonModule
  ],
  providers: [
    ApiService,
    RuleService,
    SpellService
  ],
  declarations: []
})
export class CoreModule { }