import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeCareComponent } from './prime-care.component';

describe('PrimeCareComponent', () => {
  let component: PrimeCareComponent;
  let fixture: ComponentFixture<PrimeCareComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrimeCareComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeCareComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
