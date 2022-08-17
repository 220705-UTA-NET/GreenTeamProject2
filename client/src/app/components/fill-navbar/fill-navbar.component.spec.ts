import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FillNavbarComponent } from './fill-navbar.component';

describe('FillNavbarComponent', () => {
  let component: FillNavbarComponent;
  let fixture: ComponentFixture<FillNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FillNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FillNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
