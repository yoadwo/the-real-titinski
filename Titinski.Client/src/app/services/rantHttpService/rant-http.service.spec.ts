import { TestBed } from '@angular/core/testing';

import { RantHttpService } from './rant-http.service';

describe('RantHttpService', () => {
  let service: RantHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RantHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
