import { TestBed } from '@angular/core/testing';

import { HttpService } from './http.service';
import { HttpClient } from "@angular/common/http";
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('HttpService', () => {
  beforeEach(() => TestBed.configureTestingModule({
	declarations: [],
      imports: [HttpClientTestingModule],
}));

  it('should be created', () => {
    const service: HttpService = TestBed.get(HttpService);
    expect(service).toBeTruthy();
  });
});
