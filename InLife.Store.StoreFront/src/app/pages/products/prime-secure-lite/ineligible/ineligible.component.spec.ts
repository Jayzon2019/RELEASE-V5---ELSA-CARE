import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IneligibleComponent } from './ineligible.component';

describe('IneligibleComponent', () =>
{
	let component: IneligibleComponent;
	let fixture: ComponentFixture<IneligibleComponent>;

	beforeEach(async(() =>
	{
		TestBed.configureTestingModule({
			declarations: [IneligibleComponent]
		})
			.compileComponents();
	}));

	beforeEach(() =>
	{
		fixture = TestBed.createComponent(IneligibleComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () =>
	{
		expect(component).toBeTruthy();
	});
});
