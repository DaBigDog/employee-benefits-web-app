import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpService } from '../services/http.service';


@Injectable({
  providedIn: 'root'
})
export class EmployeeListService {

  private readonly baseUrl = 'api/Employee';
  private readonly benefitsUrl = 'api/Benefit'

  constructor(private http: HttpService) {

  }

  // Gets employee list with dependents
  public getEmployeeAndDependentList(): Observable<any> {
    return this.http.sendGetRequest(this.baseUrl);
  }

  // Gets benefit deduction costs
  public getBenefitDeductionCost(): Observable<any> {
    return this.http.sendGetRequest(`${this.benefitsUrl}/DeductionCost`);
  }


  // Posts employee data list
  public saveData(data: any[]): Observable<any> {

    console.log(JSON.stringify(data));

    return this.http.sendPostRequest(`${ this.baseUrl }/list`, data);
  }

}
