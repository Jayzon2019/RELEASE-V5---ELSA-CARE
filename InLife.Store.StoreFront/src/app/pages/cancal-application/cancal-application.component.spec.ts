import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancalApplicationComponent } from './cancal-application.component';

describe('CancalApplicationComponent', () => {
  let component: CancalApplicationComponent;
  let fixture: ComponentFixture<CancalApplicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CancalApplicationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CancalApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
