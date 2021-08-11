export const environment =
{
	production: true,
	appApi:
	{
		host: 'https://www.inlifestore.com.ph/api',
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
		savePaymentEndpoint: '/SavePayment',
		quoteEndpoint: '/prime-care/applications'
	},

	affiliate:
	{
		clientID: 'f0b5e66c604f9a3b7868',
		clientSecret: '0286b6b4fa642141831cde4ba6ae7675330a206ee811df8b44e10f0e743c'
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

	paymentGatewayEndpoint: 'https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx'
};
