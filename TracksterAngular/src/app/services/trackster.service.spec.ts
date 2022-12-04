import { TestBed } from '@angular/core/testing';

import { TracksterService } from './trackster.service';

describe('TracksterService', () => {
  let service: TracksterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TracksterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
