export class Person {
  public id: number = 0;
  public firstName: string;
  public middleName: string;
  public lastName: string;
}


export class Dependent extends Person {

}

export class Employee extends Person {

  public dependents: Dependent[] = new Array<Dependent>();
}


export class DeductionCosts {
  public employeeYearlyCost: number

  public employeeYearlyCostWithDiscount: number;

  public dependentYearlyCost: number;

  public dependentYearlyCostWithDiscount: number;


  public employeeBiWeeklyCost: number

  public employeeBiweeklyCostWithDiscount: number;

  public dependentBiweeklyCost: number;

  public dependentBiweeklyCostWithDiscount: number;


  public deductionMatch: string;
}
