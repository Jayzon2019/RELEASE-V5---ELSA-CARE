import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenceMessageComponent } from './reference-message.component';

describe('ReferenceMessageComponent', () => {
  let component: ReferenceMessageComponent;
  let fixture: ComponentFixture<ReferenceMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferenceMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferenceMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
