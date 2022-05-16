import { Component, OnInit } from '@angular/core';
import { CowService } from 'src/app/services/cow.service';
import { Cow } from '../../Cow';

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
}
