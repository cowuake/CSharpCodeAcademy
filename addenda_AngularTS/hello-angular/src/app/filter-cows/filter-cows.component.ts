import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-filter-cows',
  templateUrl: './filter-cows.component.html',
  styleUrls: ['./filter-cows.component.css']
})
export class FilterCowsComponent implements OnInit {
  nameFilter: string = "";
  originFilter: string = "";
  purposeFilter: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
