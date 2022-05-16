import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Cow } from 'src/app/Cow'

@Component({
  selector: 'app-add-cow',
  templateUrl: './add-cow.component.html',
  styleUrls: ['./add-cow.component.css'],
})
export class AddCowComponent implements OnInit {
  @Output() onAddCow: EventEmitter<Cow> = new EventEmitter();
  name: string = '';
  origin: string = '';
  purpose: string = '';
  favorite: boolean = false;

  constructor() {}

  ngOnInit(): void {}

  onSubmit() {
    if (!this.name) {
      alert('Please add a cow name!');
      return;
    }

    const newCow = {
      name: this.name,
      origin: this.origin,
      purpose: this.purpose.trim().split(','),
      favorite: this.favorite,
    };

    this.onAddCow.emit(newCow);

    this.name = '';
    this.origin = '';
    this.purpose = '';
    this.favorite = false;
  }
}
