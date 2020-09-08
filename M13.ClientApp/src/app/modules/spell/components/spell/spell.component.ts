import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { SpellService } from '../../../../core/services';

@Component({
  selector: 'app-spell-page',
  templateUrl: '../../pages/spell/spell.component.html'
})
export class SpellComponent implements OnInit {
  spellErrorsControl = new FormControl();
  spellErrorsCountControl = new FormControl();

  errorsResult: string[];
  errorsResultCount: number;

  constructor(
    private spellService: SpellService
  ) { }

  ngOnInit() {
  }

  errors() {
    this.spellService
      .errors(this.spellErrorsControl.value)
      .subscribe(
        errorsResult => {
          this.errorsResult = errorsResult;
        }
      );
  }

  errorsCount() {
    this.spellService
      .errorsCount(this.spellErrorsCountControl.value)
      .subscribe(
        errorsResultCount => {
          this.errorsResultCount = errorsResultCount;
        }
      );
  }
}