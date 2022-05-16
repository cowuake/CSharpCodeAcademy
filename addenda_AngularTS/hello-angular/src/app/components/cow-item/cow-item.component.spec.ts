import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CowItemComponent } from './cow-item.component';

describe('CowItemComponent', () => {
  let component: CowItemComponent;
  let fixture: ComponentFixture<CowItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CowItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CowItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
