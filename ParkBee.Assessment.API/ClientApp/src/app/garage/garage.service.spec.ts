/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { GarageService } from './garage.service';

describe('Service: Garage', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GarageService]
    });
  });

  it('should ...', inject([GarageService], (service: GarageService) => {
    expect(service).toBeTruthy();
  }));
});
