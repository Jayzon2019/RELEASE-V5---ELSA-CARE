import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentRecievedComponent } from './payment-recieved.component';

describe('PaymentRecievedComponent', () => {
  let component: PaymentRecievedComponent;
  let fixture: ComponentFixture<PaymentRecievedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentRecievedComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentRecievedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
