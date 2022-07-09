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
  billForm: FormGroup;
  constructor(public fb: FormBuilder) {
    this.billForm = fb.group({
      name: fb.control(''),
      firstDay: fb.control(null, [Validators.required]),
      lastDay: fb.control(null, [Validators.required]),
      amount: fb.control(0, [Validators.required]),
      tenants: fb.array([]),
    });
    this.addTenant();
  }

  ngOnInit(): void {}

  get tenants() {
    return this.billForm.controls['tenants'] as FormArray;
  }

  addTenant(): void {
    const tenantForm = this.fb.group({
      firstName: this.fb.control('', [Validators.required]),
      lastName: this.fb.control(''),
      daysOff: this.fb.control(0),
      daysOfBilling: this.fb.control(0),
      dueAmount: this.fb.control(0),
    });

    this.tenants.push(tenantForm);
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
