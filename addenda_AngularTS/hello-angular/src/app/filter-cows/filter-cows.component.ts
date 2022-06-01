import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UiService } from '../services/ui.service';

@Component({
  selector: 'app-filter-cows',
  templateUrl: './filter-cows.component.html',
  styleUrls: ['./filter-cows.component.css']
})
export class FilterCowsComponent implements OnInit {
  nameFilter: string = "";
  originFilter: string = "";
  purposeFilter: string = "";
  showFilterCows: boolean = false;
  subscription: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService
      .onToggle()
      .subscribe((value) => (this.showFilterCows = !value));
  }

  ngOnInit(): void {
  }

}
