import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CowGameComponent } from './cow-game.component';

describe('CowGameComponent', () => {
  let component: CowGameComponent;
  let fixture: ComponentFixture<CowGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CowGameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CowGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
