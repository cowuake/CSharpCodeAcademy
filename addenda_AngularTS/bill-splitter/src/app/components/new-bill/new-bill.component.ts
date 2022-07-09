import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormArray,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { IBill } from 'src/app/IBill';

@Component({
  selector: 'app-new-bill',
  templateUrl: './new-bill.component.html',
  styleUrls: ['./new-bill.component.css'],
})
export class NewBillComponent implements OnInit {
  billForm: FormGroup = this.fb.group({
    name: [''],
    firstDay: [new Date(), Validators.required],
    lastDay: [new Date(), Validators.required],
    amount: [0, Validators.required],
    tenants: this.fb.array([]),
  });

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {}

  get tenants() {
    return this.billForm.controls['tenants'] as FormArray;
  }

  addTenant(): void {
    const tenantForm: FormGroup = this.fb.group({
      firstName: ['', Validators.required],
      lastName: [''],
      daysOff: [0],
      daysOfBilling: [0],
      dueAmount: [0],
    });

    this.tenants.push(tenantForm);
  }

  removeTenant(index: number): void {
    this.tenants.removeAt(index);
  }

  OnSubmit() {
    const newBill: IBill = {
      name: this.billForm.value.name,
      firstDay: this.billForm.value.firstDay,
      lastDay: this.billForm.value.lastDay,
      amount: this.billForm.value.amount,
      tenants: this.billForm.value.tenants,
    };
  }
}
