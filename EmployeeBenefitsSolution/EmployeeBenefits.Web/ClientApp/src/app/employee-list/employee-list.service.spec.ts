import { TestBed } from '@angular/core/testing';

import { EmployeeListService } from './employee-list.service';
import { HttpService } from '../services/http.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import {HttpClientModule} from '@angular/common/http'



describe('EmployeeListService', () => {
  beforeEach(() => TestBed.configureTestingModule({
imports: [
        HttpClientModule,
      ],
	providers: [ HttpService ],
}));

  it('should be created', () => {
    const service: EmployeeListService = TestBed.get(EmployeeListService);
    expect(service).toBeTruthy();
  });
});
