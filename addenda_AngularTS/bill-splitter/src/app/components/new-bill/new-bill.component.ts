import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { IBill } from 'src/app/IBill';
import { ITenant } from 'src/app/ITenant';

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
    tenants: this.fb.array<FormGroup>([]),
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
    }) as FormGroup;

    this.tenants.push(tenantForm);
  }

  removeTenant(index: number): void {
    this.tenants.removeAt(index);
  }

  clearTenants(): void {
    this.tenants.clear();
    this.addTenant();
  }

  onSubmit() {
    const newBill: IBill = {
      name: this.billForm.value.name as string,
      firstDay: this.billForm.value.firstDay as Date,
      lastDay: this.billForm.value.lastDay as Date,
      amount: this.billForm.value.amount as number,
      tenants: this.billForm.value.tenants as ITenant[],
    };
    console.log(newBill);
  }
}
