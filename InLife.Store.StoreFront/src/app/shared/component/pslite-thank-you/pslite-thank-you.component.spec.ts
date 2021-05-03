import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PSLiteThankYouComponent } from './pslite-thank-you.component';

describe('PSLiteThankYouComponent', () =>
{
	let component: PSLiteThankYouComponent;
	let fixture: ComponentFixture<PSLiteThankYouComponent>;

	beforeEach(async(() =>
	{
		TestBed.configureTestingModule({
			declarations: [PSLiteThankYouComponent]
		})
			.compileComponents();
	}));

	beforeEach(() =>
	{
		fixture = TestBed.createComponent(PSLiteThankYouComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () =>
	{
		expect(component).toBeTruthy();
	});
});
