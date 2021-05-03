import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeSecureLiteSidebarComponent } from './prime-secure-lite-sidebar.component';

describe('PrimeSecureLiteSidebarComponent', () => {
  let component: PrimeSecureLiteSidebarComponent;
  let fixture: ComponentFixture<PrimeSecureLiteSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrimeSecureLiteSidebarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeSecureLiteSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
