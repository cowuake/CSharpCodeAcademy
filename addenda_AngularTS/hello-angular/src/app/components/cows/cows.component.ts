import { Component, OnInit } from '@angular/core';
import { Cow } from '../../Cow';
import { COWS } from '../../mock-cows';

@Component({
  selector: 'app-cows',
  templateUrl: './cows.component.html',
  styleUrls: ['./cows.component.css']
})
export class CowsComponent implements OnInit {
  cows: Cow[] = COWS; // Our mock data

  constructor() { }

  ngOnInit(): void {
  }

}
