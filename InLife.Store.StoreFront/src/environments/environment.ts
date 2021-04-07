// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment =
{
	production: false,

	appApi:
	{
		// host: 'https://dev-inlife-estore.azurewebsites.net/api',
		host: 'https://localhost:44328',
		quotesEndpoint: '/quotes',
		ordersEndpoint: '/orders',
		affiliatesEndpoint: '/affiliates'
	},

	primeCareApi:
	{
		subscriptionKey: 'a149d58ebd14432ea950e802a6d32783',
		host: 'https://apim-uat.insularlife.com.ph/pg/v4',
		createQuoteEndpoint: '/CreateUnderwritingStatus',
		createApplicationEndpoint: '/CreateApplication',
		savePaymentEndpoint: '/SavePayment'
	},

	groupApi:
	{
		quoteEndpoint: '/group/applications',
		applicationEndpoint: '/group/applications/{refcode}',
		uploadFileEndpoint: '/group/applications/{refcode}/files/{type}',
		feedbackEndpoint: '/group/applications/{refcode}/feedback',
		summaryEndpont: '/group/applications/{refcode}/summary',
		statusEndpoint: '/group/applications/{refcode}/status',
		requestOtpEndpoint: '/group/applications/{refcode}/request-otp',
		requestSessionEndpoint: '/api/group/applications/{refcode}/request-session?otp={otp}'
	},

	primeSecureApi:
	{
		quoteEndpoint: '/prime-secure/applications',
		applicationEndpoint: '/prime-secure/applications/{refcode}',
		uploadFileEndpoint: '/prime-secure/applications/{refcode}/files/{type}',
		feedbackEndpoint: '/prime-secure/applications/{refcode}/feedback',
		summaryEndpont: '/prime-secure/applications/{refcode}/summary',
		statusEndpoint: '/prime-secure/applications/{refcode}/status',
		requestOtpEndpoint: '/prime-secure/applications/{refcode}/request-otp',
		requestSessionEndpoint: '/api/group/applications/{refcode}/request-session?otp={otp}'
	},

	/*paymentGatewayEndpoint: 'https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx'*/
	paymentGatewayEndpoint: 'http://projectgrey.net/fake-payment-gateway.html'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.

