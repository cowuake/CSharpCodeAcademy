import { Component, OnInit, Input } from '@angular/core';
import { COWS } from 'src/app/mock-cows';
import { Cow } from '../../Cow';

@Component({
  selector: 'app-cow-item',
  templateUrl: './cow-item.component.html',
  styleUrls: ['./cow-item.component.css'],
})
export class CowItemComponent implements OnInit {
  // Requires "strictPropertyInitialization": false in tsconfig.json
  // If could be undefined, it would not require initialization!
  @Input() cow: Cow = COWS[0];

  constructor() {}

  ngOnInit(): void {}
}
