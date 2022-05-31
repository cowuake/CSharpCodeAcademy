import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterCowsComponent } from './filter-cows.component';

describe('FilterCowsComponent', () => {
  let component: FilterCowsComponent;
  let fixture: ComponentFixture<FilterCowsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilterCowsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterCowsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
