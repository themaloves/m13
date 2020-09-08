import { Injectable } from '@angular/core';

import { ApiService } from './api.service';

@Injectable()
export class SpellService {
  constructor (
    private apiService: ApiService
  ) {}

  errors(page) {
    return this.apiService.get(`spell/errors/${page}`);
  }

  errorsCount(page) {
    return this.apiService.get(`spell/errorsCount/${page}`);
  }
}