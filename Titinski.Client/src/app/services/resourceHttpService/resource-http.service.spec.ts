import { TestBed } from '@angular/core/testing';

import { ResourceHttpService } from './resource-http.service';

describe('ResourceHttpService', () => {
  let service: ResourceHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResourceHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
