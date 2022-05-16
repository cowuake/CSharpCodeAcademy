import { Component, OnInit } from '@angular/core';
import { CowService } from 'src/app/services/cow.service';
import { Cow } from 'src/app/Cow';

@Component({
  selector: 'app-cows',
  templateUrl: './cows.component.html',
  styleUrls: ['./cows.component.css'],
})
export class CowsComponent implements OnInit {
  cows: Cow[] = [];

  constructor(private cowService: CowService) {}

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
}
