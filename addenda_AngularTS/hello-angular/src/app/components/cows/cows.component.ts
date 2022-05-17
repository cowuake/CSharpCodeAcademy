import { Component, OnInit } from '@angular/core';
import { CowService } from 'src/app/services/cow.service';
import { Cow } from 'src/app/Cow';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-cows',
  templateUrl: './cows.component.html',
  styleUrls: ['./cows.component.css'],
})
export class CowsComponent implements OnInit {
  title: string = 'All your cows';
  cows: Cow[] = [];
  showAddCow: boolean = false;
  subscription: Subscription;

  // Accessibilty to be specified for every function argument
  constructor(private cowService: CowService, private uiService: UiService) {
    this.subscription = this.uiService
      .onToggle()
      .subscribe((value) => (this.showAddCow = value));
  }

  ngOnInit(): void {
    this.cowService.getCows().subscribe((cows) => (this.cows = cows));
  }

  deleteCow(cow: Cow) {
    this.cowService
      .deleteCow(cow)
      .subscribe(() => this.cows.filter((c) => c.id !== cow.id));
  }

  toggleFavorite(cow: Cow) {
    if (cow.favorite == undefined || cow.favorite == false) {
      cow.favorite = true;
    } else {
      cow.favorite = false;
    }

    // This will actually update db.json
    this.cowService.updateCowFavorite(cow).subscribe();
  }

  addCow(cow: Cow) {
    this.cowService.addCow(cow).subscribe((cow) => this.cows.push(cow));
  }

  toggleAddCow() {
    this.uiService.toggleAddTask();
  }
}
