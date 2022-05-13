import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GifContainerComponent } from './gif-container.component';

describe('GifContainerComponent', () => {
  let component: GifContainerComponent;
  let fixture: ComponentFixture<GifContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GifContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GifContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
