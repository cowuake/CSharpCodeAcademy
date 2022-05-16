import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCowComponent } from './add-cow.component';

describe('AddCowComponent', () => {
  let component: AddCowComponent;
  let fixture: ComponentFixture<AddCowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
