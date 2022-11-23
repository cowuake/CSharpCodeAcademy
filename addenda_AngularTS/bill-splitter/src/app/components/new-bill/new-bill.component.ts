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
    firstDay: [new Date()],
    lastDay: [new Date()],
    daysOfBilling: [0],
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
    this.computeShares();
  }

  updateDaysOfBilling() {
    const diff = this.computeDateDiff(
      this.billForm.value.firstDay,
      this.billForm.value.lastDay);
    this.billForm.get('daysOfBilling')?.setValue(diff);
  }

  computeDateDiff(initialDate: Date, finalDate: Date) {
    const millisecondsInDay = 1000 * 60 * 60 * 24;
    const days = 1 + Math.abs(((new Date(finalDate)).getTime() - (new Date(initialDate)).getTime()) / millisecondsInDay);
    return days;
  }

  computeTenantDaysOfBilling(i: number) {
  }

  computeShares(): void {
  }
}
