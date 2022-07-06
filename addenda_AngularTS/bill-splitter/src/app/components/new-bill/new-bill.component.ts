import { Component, OnInit } from '@angular/core';
import { ITenant } from 'src/app/ITenant';
import { first } from 'rxjs';

@Component({
  selector: 'app-new-bill',
  templateUrl: './new-bill.component.html',
  styleUrls: ['./new-bill.component.css'],
})
export class NewBillComponent implements OnInit {
  name?: string = '';
  firstDay: Date = new Date();
  lastDay: Date = new Date();
  amount: number = 0;
  tenants: ITenant[] = [];

  constructor() {}

  ngOnInit(): void {}

  OnSubmit() {
    const newBill = {
      name: this.name,
      firstDay: this.firstDay,
      lastDay: this.lastDay,
      amount: this.amount,
      tenant: this.tenants,
    };

    this.name = '';
    this.firstDay = new Date();
    this.lastDay = new Date();
    this.amount = 0;
    this.tenants = [];
  }
}
