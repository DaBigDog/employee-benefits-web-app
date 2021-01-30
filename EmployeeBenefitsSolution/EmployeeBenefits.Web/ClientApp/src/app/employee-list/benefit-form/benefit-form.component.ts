import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subject } from 'rxjs';

import { Employee, Dependent, Person } from '../employee-list.model';

declare var $: any;

@Component({
  selector: 'benefit-form',
  templateUrl: './benefit-form.component.html',
  styleUrls: ['./benefit-form.component.css']
})
export class BenefitFormComponent implements OnInit {

  // added for unit testing reference
  @ViewChild("benefitForm", { static: false }) public benefitForm: NgForm;

  @Input() public modalTitle: string = "";
  @Input() public openModal: Subject<any> = new Subject<any>();

  @Output() public onAddResponse: EventEmitter<any> = new EventEmitter<any>();

  private person: Person = new Person();
  private addType: number;

  private modalId: string = 'benefit-form';

  constructor() { }

  ngOnInit() {

    this.openModal.subscribe((e: any) => {
      this.addType = e;
      this.person = new Person();
      this.openModalDialog();
    });

  }



  // opens modal dialog
  private openModalDialog() {
    $(`#${this.modalId}`).modal("show");
  }

  // hides modal dialog
  private closeModalDialog() {
    $(`#${ this.modalId }`).modal("hide");
  }

  // form submit event
  onSubmit(): void {
    if (this.benefitForm.valid) {
      this.onAddResponse.next({ type: this.addType, person: Object.assign({}, this.person) });
      this.closeModalDialog();
      this.resetForm();
    }
  }

  // response to close event
  onClose(): void {
    this.resetForm();

  }

  // reset form to original state.
  private resetForm(): void {
    this.benefitForm.resetForm();

    this.person = new Person();
    this.addType = null;
  }

}
