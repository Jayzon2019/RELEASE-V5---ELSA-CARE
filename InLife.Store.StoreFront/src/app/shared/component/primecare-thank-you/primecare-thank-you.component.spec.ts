import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeCareThankYouComponent } from './primecare-thank-you.component';

describe('PrimeCareThankYouComponent', () =>
{
	let component: PrimeCareThankYouComponent;
	let fixture: ComponentFixture<PrimeCareThankYouComponent>;

	beforeEach(async(() =>
	{
		TestBed.configureTestingModule({
			declarations: [PrimeCareThankYouComponent]
		})
			.compileComponents();
	}));

	beforeEach(() =>
	{
		fixture = TestBed.createComponent(PrimeCareThankYouComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () =>
	{
		expect(component).toBeTruthy();
	});
});
