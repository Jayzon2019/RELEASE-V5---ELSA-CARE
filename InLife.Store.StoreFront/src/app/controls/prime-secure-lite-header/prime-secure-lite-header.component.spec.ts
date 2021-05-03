import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeSecureLiteHeaderComponent } from './prime-secure-lite-header.component';

describe('PrimeSecureLiteHeaderComponent', () => {
  let component: PrimeSecureLiteHeaderComponent;
  let fixture: ComponentFixture<PrimeSecureLiteHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrimeSecureLiteHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeSecureLiteHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
