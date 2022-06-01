import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UiService } from '../services/ui.service';

@Component({
  selector: 'app-filter-cows',
  templateUrl: './filter-cows.component.html',
  styleUrls: ['./filter-cows.component.css'],
})
export class FilterCowsComponent implements OnInit {
  nameFilter: string = '';
  originFilter: string = '';
  purposeFilter: string = '';
  showFilterCows: boolean = true;
  subscription: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService
      .onToggle()
      .subscribe((value) => (this.showFilterCows = !value));
  }

  ngOnInit(): void {}

  updateNameFilter(): void {
    this.uiService.changeNameFilter(this.nameFilter);
  }

  updateOriginFilter(): void {
    this.uiService.changeOriginFilter(this.originFilter);
  }

  updatePurposeFilter(): void {
    this.uiService.changePurposeFilter(this.purposeFilter);
  }
}
