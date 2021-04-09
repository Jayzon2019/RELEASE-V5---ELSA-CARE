export const PaymentTransactionResponse =
[
	{ id: "?", name: "Response Unknown" },
	{ id: "0", name: "Transaction Successful" },
	{ id: "1", name: "Transaction Declined" },
	{ id: "2", name: "Bank Declined Transaction" },
	{ id: "3", name: "No Reply from Bank" },
	{ id: "4", name: "Expired Card" },
	{ id: "5", name: "Insufficient Funds" },
	{ id: "6", name: "Error Communicating with Bank" },
	{ id: "7", name: "Payment Server detected an error" },
	{ id: "8", name: "Transaction Type Not Supported" },
	{ id: "9", name: "Bank declined transaction (Do not contact Bank)" },
	{ id: "A", name: "Transaction Aborted" },
	{ id: "B", name: "Transaction Blocked - The Verification Security Level of the 3-D Secure transaction is insufficient to allow processing to continue." },
	{ id: "C", name: "Transaction Cancelled" },
	{ id: "D", name: "Deferred transaction has been received and is awaiting processing" },
	{ id: "E", name: "Issuer Returned a Referral Response" },
	{ id: "F", name: "3-D Secure Authentication failed" },
	{ id: "I", name: "Card Security Code verification failed" },
	{ id: "L", name: "Shopping Transaction Locked (Please try the transaction again later)" },
	{ id: "N", name: "Cardholder is not enrolled in Authentication scheme" },
	{ id: "P", name: "Transaction has been received by the Payment Adaptor and is being processed" },
	{ id: "R", name: "Transaction was not processed - Reached limit of retry attempts allowed" },
	{ id: "S", name: "Duplicate SessionID" },
	{ id: "T", name: "Address Verification Failed" },
	{ id: "U", name: "Card Security Code Failed" },
	{ id: "V", name: "Address Verification and Card Security Code Failed" }
];