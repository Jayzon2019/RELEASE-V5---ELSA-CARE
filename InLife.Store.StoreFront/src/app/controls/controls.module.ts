import { RouterModule } from '@angular/router';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule }        from '@angular/common';
import { InnerHeaderComponent } from './inner-header/inner-header.component';
import { InnerSidebarComponent } from './inner-sidebar/inner-sidebar.component';
import { PrimeSecureLiteSidebarComponent } from './prime-secure-lite-sidebar/prime-secure-lite-sidebar.component';
import { PrimeSecureLiteHeaderComponent } from './prime-secure-lite-header/prime-secure-lite-header.component';

@NgModule
({
	declarations:
	[
		InnerHeaderComponent,
		InnerSidebarComponent,
		PrimeSecureLiteSidebarComponent,
		PrimeSecureLiteHeaderComponent

	],
	exports:
	[
		InnerHeaderComponent,
		InnerSidebarComponent,
    	PrimeSecureLiteSidebarComponent,
		PrimeSecureLiteHeaderComponent
	],
	imports:
	[
		CommonModule,
		RouterModule
	],
	providers: [],
	bootstrap: []
})
export class ControlsModule { }
