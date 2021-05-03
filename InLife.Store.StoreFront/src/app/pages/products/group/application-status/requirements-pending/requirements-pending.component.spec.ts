import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequirementsPendingComponent } from './requirements-pending.component';

describe('RequirementsPendingComponent', () => {
  let component: RequirementsPendingComponent;
  let fixture: ComponentFixture<RequirementsPendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequirementsPendingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequirementsPendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
