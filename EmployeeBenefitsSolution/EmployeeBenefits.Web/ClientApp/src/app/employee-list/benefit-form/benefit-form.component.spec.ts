import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, NgForm } from '@angular/forms';

import { BenefitFormComponent } from './benefit-form.component';

describe('BenefitFormComponent', () => {
  let component: BenefitFormComponent;
  let fixture: ComponentFixture<BenefitFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BenefitFormComponent ],
imports: [FormsModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BenefitFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    fixture.whenStable().then(() => {

      component.benefitForm.controls['firstName'].setValue('test_first');
      component.benefitForm.controls['lastName'].setValue('test_last');
    })
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('is valid form', () => {
    console.log(component.benefitForm);

    expect(component.benefitForm.valid).toBe(true);
  });





});
