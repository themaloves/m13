import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { RuleGetModel } from '../../shared/models';

@Injectable()
export class RuleService {
  constructor (
    private apiService: ApiService
  ) {}

  add(site, rule) {
    const body = { site, rule };

    return this.apiService.post(`rules/add`, body);
  }

  get(site): Observable<RuleGetModel> {
    return this.apiService
      .get(`rules/get/${site}`)
      .pipe(data => data);
  }

  delete(site) {
    const body = { site };

    return this.apiService.post('rules/delete', body);
  }

  test(page, rule) {
    const body = { page, rule };

    return this.apiService.post('rules/test', body);
  }
}