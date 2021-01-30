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

  constructor() { }

  ngOnInit() {

    this.openModal.subscribe((e: any) => {
      this.addType = e;
      this.person = new Person();
      this.openModalDialog();
    });

  }




  private openModalDialog() {
    $('#benefit-form').modal("show");
  }

  private closeModalDialog() {
    $('#benefit-form').modal("hide");
  }


  submit(): void {
    if (this.benefitForm.valid) {
      this.onAddResponse.next({ type: this.addType, person: Object.assign({}, this.person) });
      this.closeModalDialog();
      this.resetForm();
    }
  }

  close(): void {
    this.resetForm();

  }

  private resetForm(): void {
    this.benefitForm.resetForm();

    this.person = new Person();
    this.addType = null;
  }

}
