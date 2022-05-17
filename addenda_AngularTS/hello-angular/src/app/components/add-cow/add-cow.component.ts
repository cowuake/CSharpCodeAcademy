import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';
import { Cow } from 'src/app/Cow';
import { Subscription } from 'rxjs';

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
  showAddCow: boolean = false;
  subscription: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService
      .onToggle()
      .subscribe((value) => (this.showAddCow = value));
  }

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
