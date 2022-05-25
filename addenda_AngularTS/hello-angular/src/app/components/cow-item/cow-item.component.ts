import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { COWS } from 'src/app/mock-cows';
import { ICow } from 'src/app/Cow';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-cow-item',
  templateUrl: './cow-item.component.html',
  styleUrls: ['./cow-item.component.css'],
})
export class CowItemComponent implements OnInit {
  @Input() cow: ICow = COWS[0];
  @Output() onDeleteCow: EventEmitter<ICow> = new EventEmitter();
  @Output() onToggleFavorite: EventEmitter<ICow> = new EventEmitter();
  faTimes = faTimes;

  constructor() {}

  ngOnInit(): void {}

  onDelete(cow: ICow) {
    this.onDeleteCow.emit(cow);
  }

  onToggle(cow: ICow) {
    this.onToggleFavorite.emit(cow);
  }
}
