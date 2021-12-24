import { TestBed } from '@angular/core/testing';

import { TriangleapiService } from './triangleapi.service';

describe('TriangleapiService', () => {
  let service: TriangleapiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TriangleapiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
