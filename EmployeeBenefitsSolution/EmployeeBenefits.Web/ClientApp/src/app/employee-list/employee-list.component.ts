import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { EmployeeListService } from './employee-list.service';
import { Employee, Dependent, Person, DeductionCosts } from './employee-list.model';

@Component({
  selector: 'employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  public isLoading: boolean = true;
  public employeeList: any[];
  public deductionCosts: DeductionCosts;
  private addToEmpDependents: Employee;

  
  private readonly empText: string = 'Employee';
  private readonly depText: string = 'Dependent';
  public modalTitle: string = this.empText;

  public sendOpenModal: Subject<any> = new Subject<any>();

  constructor(private service: EmployeeListService) {
  }

  ngOnInit() {
    this.loadEmployeeListData();
    this.loadDeductionCostData();
  }


// Gets Employee data
  private loadEmployeeListData() {

// show is loading message
    this.isLoading = true;

    this.service.getEmployeeAndDependentList()
      .pipe(finalize(() => {
        // hide is loading message
        this.isLoading = false;
      }))
      .subscribe((data: any) => {
      this.employeeList = data;

      console.log(data);
    }, (error: any) => {
        console.log(error);
    });

  }

  // Gets deduction cost data
  private loadDeductionCostData() :void {

    this.service.getBenefitDeductionCost().subscribe((data: any) => {
      this.deductionCosts = data;

      console.log(data);
    }, (error: any) => {
      console.log(error);
    });

  }

  // saves changes made to the Employee list
  onSave(): void {
    this.service.saveData(this.employeeList).subscribe((data: any) => {
      // notify the user...
      alert('Employee list successfully saved.');
      console.log(data);
    }, (error: any) => {
        alert(`I'm sorry but an error occurred saving the Employee list.`);
      console.log(error);
    });

  }

  // add employee event
  onAddEmployee(e: any): void {
    this.modalTitle = this.empText;
    this.sendOpenModal.next(1);
  }

  
  // add dependent event
  onAddDependent(e: any): void {
    this.modalTitle = this.depText;
    this.sendOpenModal.next(2);
    this.addToEmpDependents = e;
    console.log(e);
  }

  // employee or dependent added
  onAddEvent(e: any): void {
    if (e.type === 1) {
      this.employeeList.push(e.person)
    } else {
      if (!this.addToEmpDependents.dependents) {
        this.addToEmpDependents.dependents = new Array<Dependent>();
      }
      this.addToEmpDependents.dependents.push(e.person);
    }
    console.log(e);
  }

  // delete employee
  onDeleteEmployee(e: any): void {
    console.log(e);
    if (this.confirmDelete(this.empText)) {
      if (e.id === 0) {
        this.employeeList = this.employeeList.filter(emp => {
          return !(emp.firstName === e.firstName && emp.middleName === e.middleName && emp.lastName === e.lastName);
        });

      } else {
        this.employeeList = this.employeeList.filter(emp => {
          return !(emp.id === e.id);
        });
      }
    }
  }

  // delete dependent event
  onDeleteDependent(e: any, d: any): void {
    console.log(e, d);
    if (this.confirmDelete(this.depText)) {
      if (d.id === 0) {
        e.dependents = e.dependents.filter(dep => {
          return !(dep.firstName === d.firstName && dep.middleName === d.middleName && dep.lastName === d.lastName);
        });

      } else {
        e.dependents = e.dependents.filter(dep => {
          return !(dep.id === d.id);
        });
      }
    }
  }


  // confirm user wants to delete
  private confirmDelete(msg: string): boolean {
    return confirm(`Are you sure to delete this ${msg}?`);
  }



/********  grid functions  *************/

  // string start with letter from deduction cost model
  private beginsWithLetter(str: string): boolean {
    if (str && this.deductionCosts.deductionMatch) {
      return (str[0].toUpperCase() === this.deductionCosts.deductionMatch.toUpperCase())
    }
    return false;
  }

  // first or last name is a match
  private nameIsMatch(p: Person) {
    return (this.beginsWithLetter(p.firstName) || this.beginsWithLetter(p.middleName) ||
      this.beginsWithLetter(p.lastName))
  }

/** Total yearly cost **/

  // gets employee yearly cost
  getEmployeeYearlyCost(emp: Employee): number {
    if (this.nameIsMatch(emp)) {
      return this.deductionCosts.employeeYearlyCostWithDiscount;
    } else {
      return this.deductionCosts.employeeYearlyCost;
    }
  }

  // gets dependent yearly cost
  getDependentYearlyCost(dep: Dependent): number {
    if (this.nameIsMatch(dep)) {
      return this.deductionCosts.dependentYearlyCostWithDiscount;
    } else {
      return this.deductionCosts.dependentYearlyCost;
    }
  }

/*** Biweekly deduction  ***/

  // gets Employee bi-weekly deduction
  getEmployeeBiweeklyCost(emp: Employee): number {
    if (this.nameIsMatch(emp)) {
      return this.deductionCosts.employeeBiweeklyCostWithDiscount;
    } else {
      return this.deductionCosts.employeeBiWeeklyCost;
    }
  }

  // gets dependent bi-weekly deduction
  getDependentBiweeklyCost(dep: Dependent): number {
    if (this.nameIsMatch(dep)) {
      return this.deductionCosts.dependentBiweeklyCostWithDiscount;
    } else {
      return this.deductionCosts.dependentBiweeklyCost;
    }
  }
}
