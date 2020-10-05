export const environment =
{
	production: true,

	appApi:
	{
		host: 'https://uat-inlife-estore-api.azurewebsites.net',
		quotesEndpoint: '/quotes',
		ordersEndpoint: '/orders',
	},

	primeCareApi:
	{
		subscriptionKey: 'a149d58ebd14432ea950e802a6d32783',
		host: 'https://apim-uat.insularlife.com.ph/pg/v4',
		createApplicationEndpoint: '/CreateApplication',
		savePaymentEndpoint: '/SavePayment',
	},

	paymentGatewayEndpoint: 'https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx'
};
