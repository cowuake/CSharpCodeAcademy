import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { COWS } from 'src/app/mock-cows';
import { Cow } from '../../Cow';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-cow-item',
  templateUrl: './cow-item.component.html',
  styleUrls: ['./cow-item.component.css'],
})
export class CowItemComponent implements OnInit {
  @Input() cow: Cow = COWS[0];
  @Output() onDeleteCow: EventEmitter<Cow> = new EventEmitter();
  @Output() onToggleFavorite: EventEmitter<Cow> = new EventEmitter();
  faTimes = faTimes;

  constructor() {}

  ngOnInit(): void {}

  onDelete(cow: Cow) {
    this.onDeleteCow.emit(cow);
  }

  onToggle(cow: Cow) {
    this.onToggleFavorite.emit(cow);
  }
}
