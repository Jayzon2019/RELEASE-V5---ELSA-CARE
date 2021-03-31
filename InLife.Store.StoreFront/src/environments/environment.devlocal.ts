export const environment =
{
	production: true,

	appApi:
	{
		host: 'https://localhost:5100',
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

	paymentGatewayEndpoint: 'http://projectgrey.net/fake-payment-gateway.html'
	/*paymentGatewayEndpoint: 'https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx'*/
};
