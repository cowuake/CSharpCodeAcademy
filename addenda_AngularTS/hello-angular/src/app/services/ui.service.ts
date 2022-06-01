import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UiService {
  private showAddCow: boolean = false;

  private nameFilterSource$ = new BehaviorSubject<string>('');
  private originFilterSource$ = new BehaviorSubject<string>('');
  private purposeFilterSource$ = new BehaviorSubject<string>('');

  public cowNameFilter = this.nameFilterSource$.asObservable();
  public cowOriginFilter = this.originFilterSource$.asObservable();
  public cowPurposeFilter = this.purposeFilterSource$.asObservable();

  private subject = new Subject<any>(); // Subjects are employed for multicasting values to multiple observers

  constructor() {}

  toggleAddCow(): void {
    this.showAddCow = !this.showAddCow;
    this.subject.next(this.showAddCow);
  }

  // See usage in 'add-cow-component.ts' and 'filter-cows.component.ts'
  onToggle(): Observable<any> {
    return this.subject.asObservable();
  }

  changeNameFilter(name: string): void {
    this.nameFilterSource$.next(name);
  }

  changeOriginFilter(origin: string): void {
    this.originFilterSource$.next(origin);
  }

  changePurposeFilter(purpose: string): void {
    this.purposeFilterSource$.next(purpose);
  }
}
