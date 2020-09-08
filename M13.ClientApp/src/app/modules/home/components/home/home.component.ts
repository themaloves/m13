import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { RuleGetModel } from '../../../../shared/models';
import { RuleService } from '../../../../core/services';

@Component({
  selector: 'app-home-page',
  templateUrl: '../../pages/home/home.component.html'
})
export class HomeComponent implements OnInit {
  siteAddControl = new FormControl();
  ruleAddControl = new FormControl();
  siteGetControl = new FormControl();
  siteDeleteControl = new FormControl();
  siteTestControl = new FormControl();
  ruleTestControl = new FormControl();

  ruleGetModel: RuleGetModel;
  testText: string;

  constructor(
    private ruleService: RuleService
  ) { }

  ngOnInit() {
  }

  addRule() {
    this.ruleService
      .add(this.siteAddControl.value, this.ruleAddControl.value)
      .subscribe(
        () => {
          this.siteAddControl.reset('');
          this.ruleAddControl.reset('');
        }
      );
  }

  getRule() {
    this.ruleService
      .get(this.siteGetControl.value)
      .subscribe(
        ruleGetModel => {
          this.ruleGetModel = ruleGetModel;
        }
      );
  }

  deleteRule() {
    this.ruleService
      .delete(this.siteDeleteControl.value)
      .subscribe(
        () => {
          this.siteDeleteControl.reset('');
        }
      );
  }

  testRule() {
    this.ruleService
      .test(this.siteTestControl.value, this.ruleTestControl.value)
      .subscribe(
        result => {
          this.testText = result.text;
          this.siteDeleteControl.reset('');
        }
      );
  }
}