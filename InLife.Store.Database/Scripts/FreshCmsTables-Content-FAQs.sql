IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_IsArchived]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_FaqCategory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TblFaqCategories]') AND type in (N'U'))
ALTER TABLE [dbo].[TblFaqCategories] DROP CONSTRAINT IF EXISTS [DF_TblFaqCategories_CreatedDate]
GO
/****** Object:  Table [dbo].[TblFaqs]    Script Date: 27 Jan 2021 1:03:17 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFaqs]
GO
/****** Object:  Table [dbo].[TblFaqCategories]    Script Date: 27 Jan 2021 1:03:17 AM ******/
DROP TABLE IF EXISTS [dbo].[TblFaqCategories]
GO
/****** Object:  Table [dbo].[TblFaqCategories]    Script Date: 27 Jan 2021 1:03:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFaqCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK_TblFaqCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFaqs]    Script Date: 27 Jan 2021 1:03:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFaqs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](36) NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[UpdatedBy] [varchar](36) NULL,
	[CategoryId] [int] NOT NULL,
	[Question] [nvarchar](300) NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[SortNum] [int] NULL,
 CONSTRAINT [PK_TblFaq] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TblFaqCategories] ON 
GO
INSERT [dbo].[TblFaqCategories] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Name], [Description], [IsActive], [IsArchived]) VALUES (3, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2020-12-17T08:03:41.0304492+00:00' AS DateTimeOffset), N'c91012be-4863-4eb3-b6cf-1d3845196951', N'General  FAQs', N'General  Frequently Asked Questions', 1, 0)
GO
INSERT [dbo].[TblFaqCategories] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Name], [Description], [IsActive], [IsArchived]) VALUES (4, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2020-12-17T08:04:11.2524300+00:00' AS DateTimeOffset), N'c91012be-4863-4eb3-b6cf-1d3845196951', N'Prime CareFAQs', N'Prime CareFrequently Asked Questions', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[TblFaqCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[TblFaqs] ON 
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2020-12-17T08:29:16.0703220+00:00' AS DateTimeOffset), N'c91012be-4863-4eb3-b6cf-1d3845196951', 3, N'Who is InLife?', N'<p> The first and largest Filipino life insurance company and the only mutual
company in the Philippines, Insular Life has been a partner in nation-building
and uplifting the lives of Filipino families since 1910. </p>
<p>We apply over 100 years of experience in financial protection, savings,
investments, and retirement to help you make confident decisions for you and
your loved ones. We recognize that financial priorities change over time — that
is why we are here to help you plan ahead, every step of the way.</p>
<p>As part of our dual transformation, we refreshed our brand identity in 2018 with
the name InLife. The refreshed brand now represents the new realities of our
workforce, products and services, and distribution channels.</p>
<p>You may visit our corporate website at <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBo" target="_blank">www.insularlife.com.ph</a> </p>', 1, 0, 1)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1002, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'What is the InLife Store?', N'<p>The InLife Store is InLife’s official and own e-commerce store (or eStore) and organic channel where you can purchase life insurance plans online through a fast and hassle-free application, with no long interview or paperwork, and safe and secured transactions. End-to-end process is 100% online. A Lifetime for Good is just a click away!</p>', 1, 0, 2)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1003, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'What are the InLife products available in the InLife Store?', N'<p>For our initial product offering, we have Prime Care, a Critical Illness
Insurance plan that provides financial security in the event of a critical
illness diagnosis so you can focus on your recovery and getting well. </p>
<p>We are also proud to introduce to you the prepaid products of Insular Health
Care, our health care subsidiary. Please take note that purchase and fulfillment
shall take place in their own online store <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly9zaG9wLmluc3VsYXJoZWFsdGhjYXJlLmNvbS5waA==" target="_blank">https://shop.insularhealthcare.com.ph</a>
</p>', 1, 0, 3)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1004, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'I’d like to know more about the Insular Health Care products.', N'<p>You may visit their online store directly or chat with them at
<a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly9zaG9wLmluc3VsYXJoZWFsdGhjYXJlLmNvbS5waA==" target="_blank">https://shop.insularhealthcare.com.ph</a> or contact their Customer Relations
Assistant Helpdesk for assistance:<br>(632) 8-813-0131 local 8364<br>(632)
8-813-0131 (Press 1)<br>24/7 support through Insular Health Care’s Call Center
(Toll Free Number 1-800-10-8177857)</p>', 1, 0, 4)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1005, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I purchase the Insular Health Care products?', N'	<p>You may purchase the IHC products through their online store at
											<a href="https://shop.insularhealthcare.com.ph" target="_blank">https://shop.insularhealthcare.com.ph</a> </p>', 1, 0, 5)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1008, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I purchase InLife products online?', N'<p style="margin-bottom: 30px;">You can purchase the InLife plans online as long as
you :</p>
<ul class="list" style="margin-bottom: 30px">
<li>are a Filipino citizen and currently residing in the Philippines. </li>
<li>have a valid government-issued ID and</li>
<li>have a credit / debit card for online payment through an easy, hassle-free
3-step online application process:</li>
</ul>
<ul>
<li>Get Quote</li>
<li>Apply</li>
<li>Pay</li>
</ul>
<p>Eligible ages shall depend on the product.</p>', 1, 0, 6)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1009, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'What is the difference between products in the InLife Store and your other products?', N'<p>The products in the InLife store can be purchased online and direct, with no
broker or financial advisor. These are simple issue product offers which means
you do not have to go through a full-underwriting procedure. You only need to
answer a few screening questions to determine your eligibility. Premium
payment/s can be made via credit card/debit card. </p>
<p>However, for plans with higher premiums or coverages, or other product options,
we suggest discussing these with a financial advisor. You may visit our main
site <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL3BlcnNvbmFsLWluc3VyYW5jZQ==" target="_blank">https://www.insularlife.com.ph/personal-insurance</a> or
fill out a form at <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL2NvbnRhY3QtdXM=" target="_blank">https://www.insularlife.com.ph/contact-us </a>for more
product information or to request for assistance from a financial advisor.&#8203;</p>', 1, 0, 7)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1011, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Are the products cheaper than the ones sold by agents?', N'<p> Following the Insurance Commission’s Guidelines on Electronic Commerce of
lnsurance Products, we can only offer products priced within the approved limit.
</p>
<p>We have also taken into consideration the price or the premium amount that online
shoppers are willing to pay or shell out using their credit/debit cards.
Moreover, all transactions are online. Our products can be purchased directly
from our store in less than 30 minutes. The official receipt and Policy Contract
are sent via your InLife Customer Portal account within 24 to 48 hours if all
requirements are submitted and complete. No additional cost on transport,
warehousing and shipping.</p>', 1, 0, 8)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1013, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Are the products cheaper than the ones sold by agents?', N'<p> Following the Insurance Commission’s Guidelines on Electronic Commerce of
											lnsurance Products, we can only offer products priced within the approved limit.
										</p>
										<p>We have also taken into consideration the price or the premium amount that online
											shoppers are willing to pay or shell out using their credit/debit cards.
											Moreover, all transactions are online. Our products can be purchased directly
											from our store in less than 30 minutes. The official receipt and Policy Contract
											are sent via your InLife Customer Portal account within 24 to 48 hours if all
											requirements are submitted and complete. No additional cost on transport,
											warehousing and shipping.</p>', 0, 0, 11)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1014, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Is it safe to buy online / to buy from the InLife Store?', N'	<p style="margin-bottom: 30px"> Our store and our payment gateway partners have
											specific measures in place to make sure all payment transactions are safe and
											secure.
											For your safety, we also advise the following</p>
										<ul>
											<li>Refer to our Customer Charter for your rights and the Company’s returns
												policy.</li>
											<li>Ensure that your software and anti-virus protection is up-to-date.</li>
											<li> Do not use public Wi-Fi when you are shopping online. </li>
											<li>Protect your information. Never give out financial information like your
												credit card or bank account number by email or SMS or chat. </li>
										</ul>', 1, 0, 9)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1015, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Is my information safe when I purchase products from InLife?', N'		<p>InLife is committed to safeguarding the privacy of its clients. We have
											implemented security measures aligned with industry standards in order to ensure
											that the confidentiality, integrity, and availability is maintained for any
											personal data that is submitted to us.</p>
										<p>For more information, you may refer to: <a href="https://www.insularlife.com.ph/privacy-policy" target="_blank">https://www.insularlife.com.ph/privacy-policy</a></p>
								', 1, 0, 10)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1016, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Can I buy on my own, or do I need a Financial Advisor to assist me?', N'<p>Because the process is easy, fast and convenient, you can buy on your own. </p>', 1, 0, 11)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1017, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Can I buy for another person?', N'<p>No, the person who buys the insurance should also be the person to be insured. ID
											submitted should also be from same person.​</p>', 1, 0, 12)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1018, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I pay for the policy?', N'<p>You may pay using your Visa or Mastercard credit card or debit card.</p>', 1, 0, 13)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1019, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Can I pay using someone else’s credit card or debit card?', N'<p>We allow the <u>authorized</u> use of credit cards of the following immediate
											family members: spouse, children, parents, brothers and sisters of the
											policyholder.</p>
										<p>For succeeding annual and monthly premiums, you may enroll at our Auto-Charging
											Option (ACO) payment facility to ensure that payments are made on or before the
											premium due date.
										</p>', 1, 0, 14)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1020, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I receive my policy?', N'	<p>Your policy contract will be sent to you via your Customer Portal account within
											24 to 48 hours (except holidays and weekends) if all requirements are submitted
											and complete.</p>', 1, 0, 15)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1021, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Can I buy if I am outside the Philippines?', N'<p>Unfortunately, the InLife Store will only accept applications from Filipinos who
											are currently in the Philippines at time of application.</p>', 1, 0, 16)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1022, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I get my policy details?', N'<p>You will be automatically enrolled to the InLife Customer Portal, InLife’s
on-line policy servicing facility, where you can access your policy information
and enjoy online end-to- end policy servicing. Your policy contract will be sent
to you via your Customer Portal account within 24 to 48 hours (except holidays
and weekends) if all requirements are submitted and complete. An InLife
representative shall get in touch with you, if necessary, for questions or
additional details. </p>
<p>You may also refer to our <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL2N1c3RvbWVyLWNoYXJ0ZXI=">Customer Charter</a>
for details.</p>', 1, 0, 17)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1024, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I change my contact and/or beneficiary details after the policy issuance?', N'<p>You may change your contact details or policy details by contacting us at: Email:
 <a href="mailto:customercare@insular.com.ph" target="_blank">customercare@insular.com.ph</a> Customer Care Hotline
Numbers (02) 8-876-1-800 for Metro Manila or 1-800-10-INSULAR (1-800-10-4678527)
Provincial Toll-Free Number Our business hours are from 7am to 5pm, Mondays to
Fridays, except Holidays.</p>
<p>You may also refer to our <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL2N1c3RvbWVyLWNoYXJ0ZXI=" target="_blank">Customer Charter</a> for details.</p>', 1, 0, 19)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1026, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I get my payment refund?', N'<p>You may request for a refund of your premium as long as you submit in writing
your request to cancel the policy within fifteen (15) days from the date of
receipt of the Policy (via e-mail).</p>
<p style="margin-bottom: 30px;">The Company''s standard operating procedures for
chargeback requests, as well as bank/credit card company guidelines, shall
apply. The crediting of the premiums paid will depend on your issuing bank</p>
<ul>
<li>credit card – five (5) to fifteen (15) banking days</li>
<li>debit card – five (5) to forty-five (45) banking days</li>
</ul>', 1, 0, 21)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1027, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Are there other payment channel options?', N'<p>For now, payments may be done through your credit card / debit card. Please watch
											out for announcements on new / additional payment facilities.</p>', 1, 0, 22)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1028, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Will I key in all details again if I decide not to proceed today and will make the purchase tomorrow?', N'<p>Yes, for security reasons, we do not store data in our site.</p>', 1, 0, 23)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1029, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Who do I contact if my transaction failed or lost internet connection?', N'<p>In case you have not submitted your application or used your credit card / debit
card to pay for the plan, you may have to start all over again.&#8203;</p>
<p style="margin-bottom: 30px">If you have submitted your application and used your
credit card / debit card to pay for the plan, you may contact:</p>
<ul>
<li>Your Bank/Credit Card Company to check if your payment was processed.</li>
<li>Our Customer Care, if you wish to confirm that your application has been
submitted or received, so they can check with the proper unit.</li>
</ul>
<p>You may also wish to check your internet connection.&#8203;</p>', 1, 0, 24)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1030, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Who do I contact if my transaction failed/keep failing?', N'<p>If you have submitted your application and used your credit card / debit card to pay for the plan, you may contact:.</p>
<ul>
<li>Your Bank/Credit Card Company to check if your payment was processed.</li>
<li>Our Customer Care, if you wish to confirm that your application has been submitted or receceived, so they can check with the proper unit. </li>
</ul>
<p>You may also wish to check your internet connection.</p>', 1, 0, 25)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1031, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Who shall I contact if I didn''t receive an email confirmation of my purchase?', N'<p>You may contact us at: Email: <a href="mailto:customercare@insular.com.ph" target="_blank">customercare@insular.com.ph</a> Customer Care Hotline
											Numbers (02) 8-876-1-800 for Metro Manila or 1-800-10-INSULAR (1-800-10-4678527)
											Provincial Toll-Free Number Our business hours are from 7am to 5pm, Mondays to
											Fridays, except Holidays.</p>', 1, 0, 26)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1032, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'If I have other questions, who shall I contact?', N'<p>You may refer to our FAQs (Frequently Asked Questions) However, in case there are
											concerns that are not readily answered or covered by the FAQs, you may contact
											us at: Email: <a href="mailto:customercare@insular.com.ph" target="_blank">customercare@insular.com.ph</a> Customer Care Hotline Numbers (02)
											8-876-1-800 for Metro Manila or 1-800-10-INSULAR (1-800-10-4678527) Provincial
											Toll-Free Number Our business hours are from 7am to 5pm, Mondays to Fridays,
											except Holidays.</p>
										<p>You may also refer to our <a href="https://www.insularlife.com.ph/customer-charter" target="_blank">Customer Charter</a> for details.</p>', 1, 0, 27)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1033, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2020-12-17T08:18:18.4402624+00:00' AS DateTimeOffset), N'c91012be-4863-4eb3-b6cf-1d3845196951', 4, N'Why do I need critical illness insurance?', N'<p>The devastating effects of critical illnesses extend from one’s health to their
											finances. Critical Illness Insurance provides funds to help cover the costs of
											treatments. </p>
										<p>In fact, the first ever Critical Illness Insurance plan was developed by a doctor
											in South Africa . He wanted to help reduce the financial stress put on survivors
											of critical illnesses. </p>', 1, 0, 31)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1034, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'What are the benefits of Prime Care?', N'<p>If you avail of Prime Care and become diagnosed with any of the 35 covered
											critical illnesses*, you will receive a lump sum equal to six (6) times the
											monthly allowance. Thereafter, you will receive 30 monthly cash allowance
											payments.</p>
										<p>You also gain access to expert medical advice and opinions via Best Doctors™.
											This is an international network of medical practitioners who have expertise in
											various medical disciplines. </p>
										<p>*subject to the 90-day waiting period upon application for Prime Care and 30-day
											survival period after diagnosis of critical illness</p>', 1, 0, 32)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1036, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'Is Total and Permanent Disability covered by Prime Care?', N'<p>If the insured is receiving the monthly cash allowance and is diagnosed with
											total and permanent disability caused by the covered critical illness, the
											insured will receive a lump sum equal to thirty-six (36) months cash allowance.
										</p>', 1, 0, 34)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1037, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'What makes Prime Care the better stand-alone Critical Illness Insurance out in the market today?', N'<p>The amount of coverage Prime Care provides better matches the actual cost of
											critical illness treatment today. In the context of cancer treatment, estimates
											for total cost of treatment go into seven-digits. Thus, you leave yourself still
											exposed to financial risk if you chose other online critical illness plans.</p>', 1, 0, 35)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1038, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'Other companies cover more than 35 critical illnesses, are they better?', N'<p>Be careful about this, some only expound on critical illnesses already covered
											while some have qualifying descriptions. One example is for cancer. While Prime
											Care covers all types of cancer, some competitors make a qualification that only
											late-stage cancers are covered. Those cancers that are detected early are not.
										</p>', 1, 0, 36)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1039, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'I am still young and healthy; do I need critical illness insurance?', N'<p>Yes, because critical illness may happen to anyone. For example, about 4% of
											cancers are diagnosed in young adults aged 20-39 .</p>
										<p>
											Prime Care Critical Illness Insurance is especially important because most young
											adults have not yet built up their savings and resources that are required to
											treat critical illnesses.
										</p>', 1, 0, 37)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1040, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'Do I need to get a medical check-up to avail of critical illness insurance?', N'<p>Prime Care E-Commerce is a Simplified Issue Product Offer which means it does not
											go thru a full-underwriting procedure. You only need to answer a few screening
											questions and you can avail of critical illness insurance.</p>', 1, 0, 38)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1041, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'How can I avail of Prime Care?', N'<p>Prime Care E-Commerce is an online-exclusive insurance plan available at <a href="https://www.inlifestore.com.ph" target="_blank">https://www.inlifestore.com.ph.</a>
										</p>
										<p style="margin-bottom: 30px;">Our website has simplified the purchase experience!
										</p>
										<ul style="margin-bottom: 30px;">
											<li>Get Quote</li>
											<li>Apply</li>
											<li>Pay online</li>
										</ul>
										<p>Your policy contract will be sent to you via your InLife Customer Portal account
											within 24 to 48 hours (except holidays and weekends) if all requirements are
											submitted and complete.</p>', 1, 0, 39)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1042, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'What documents are required to complete my Prime Care application online?', N'<p style="margin-bottom: 30px;">Aside from a Filipino citizenship and residence in
											the Philippines at time of application, you will need the following: </p>
										<ul class="list" style="margin-bottom: 30px;">
											<li>A valid government-issued ID (kindly ensure that name and birthday match the
												application)</li>
											<li>Your preferred mode of payment (Credit and Debit Cards are accepted)</li>
										</ul>
										<p>*If you select monthly premium payment, you will need to enroll your credit card
											for the auto charge option via the InLife Customer Portal.</p>', 1, 0, 40)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1043, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'Is there a grace period to pay for subsequent premiums?', N'	<p>After payment of the initial premium, a grace period of thirty-one (31) days will
											be granted for the payment of each premium falling due after the first premium.
											If the Policy owner is unable to pay the premium within the grace period, the
											policy shall automatically terminate. If the diagnosis of the Critical Illness
											occurs within the grace period, any premium then due and unpaid will be deducted
											from payable benefits.
										</p>', 1, 0, 41)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1044, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'Can I purchase Prime Care online for someone?', N'										<p>While you may help someone get critical insurance coverage through the Insular
											Life online store, applicant-owner and insured should be one and the same
											person. ID presented should also be from same person.</p>
', 1, 0, 42)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1045, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'How do I file a claim upon getting a critical illness diagnosis?', N'<p>Prime Care claims follow the regular claims procedures. Go to <a href="https://www.insularlife.com.ph/policyholders-servicing-requirements" target="_blank">Policy Servicing Requirements</a> in <a href="https://www.insularlife.com.ph/" target="_blank">www.insularlife.com.ph</a> for more
											details.</p>', 1, 0, 43)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1046, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'How long can I renew my critical illness insurance?', N'<p>Prime Care online issue ages are 18-60, but you can renew your critical illness
											plan up to age 69. Each renewal premium is based on the attained age of the
											insured.</p>
										<p>
											The Company will notify you regarding the renewal of your Policy and your
											renewal premium. The renewal premium may be adjusted based on the attained age
											of the Insured.
										</p>', 1, 0, 44)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1047, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'If I decide not to proceed with Prime Care can I request for a refund?', N'<p>You may request for a refund of your premium as long as you submit in writing,
											your request to cancel the policy within fifteen (15) days from the date of
											receipt of the Policy (via e-mail).</p>', 1, 0, 45)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1048, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'How do I make a claim?', N'<p>For claims procedures, please go to <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL3BvbGljeWhvbGRlcnMtc2VydmljaW5nLXJlcXVpcmVtZW50cw==" target="_blank">Policy Servicing Requirements </a>in <a href="https://proxysite.cloud/?cdURL=aHR0cDovL3d3dy5pbnN1bGFybGlmZS5jb20ucGgv" target="_blank">www.insularlife.com.ph</a> for more
details. Or you may contact us at:
Email: <a href="mailto:customercare@insular.com.ph" target="_blank">customercare@insular.com.ph</a> Customer Care Hotline Numbers (02)
8-876-1-800 for Metro Manila or 1-800-10-INSULAR (1-800-10-4678527) Provincial
Toll-Free Number Our business hours are from 7am to 5pm, Mondays to Fridays,
except Holidays.
</p>
<p>You may also refer to our <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL2N1c3RvbWVyLWNoYXJ0ZXI=" target="_blank">Customer Charter</a> for details.</p>
', 1, 0, 18)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1049, CAST(N'2020-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'Can I cancel my policy anytime with full refund of premium?', N'<p style="margin-bottom: 30px;">Request for cancellations shall be subject to the
Free Look Provision of your Policy Contract. For cancellation
requests/inquiries, please refer to your Policy Contract or Proof of Cover for
the applicable service unit to contact or get in touch with our Customer Care at
<a href="mailto:customercare@insular.com.ph" target="_top">customercare@insular.com.ph</a> or call (632) 8-878-1818. We
will advise you of the steps to take and/or the forms (including electronic
forms) to accomplish and submit to us so we can process your requests or
instructions.</p>
<ul>
<li>If your request is received within the cancellation period, you will receive
a notification letter and/or email confirming the cancellation of your
insurance cover.</li>
<li>The Company''s standard operating procedures for chargeback requests, as well
as bank/credit card company guidelines, shall apply.</li>
</ul>
<p>You may also refer to our <a href="https://proxysite.cloud/?cdURL=aHR0cHM6Ly93d3cuaW5zdWxhcmxpZmUuY29tLnBoL2N1c3RvbWVyLWNoYXJ0ZXI=">Customer Charter</a>
for details.</p>', 1, 0, 20)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1050, CAST(N'2020-12-17T11:39:09.2723080+00:00' AS DateTimeOffset), NULL, NULL, NULL, 3, N'NOTE:', N'NOTE: With the Company operating with limited resources during the Enhanced Community Quarantine, please be advised that our office hours in the meantime are from 8:00 am to 4:00 pm, Mondays to Fridays, except holidays. Kindly bear with us as there may be some delays in our servicing.', 1, 0, 28)
GO
INSERT [dbo].[TblFaqs] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [CategoryId], [Question], [Answer], [IsActive], [IsArchived], [SortNum]) VALUES (1051, CAST(N'2020-12-17T11:39:09.2723080+00:00' AS DateTimeOffset), NULL, NULL, NULL, 4, N'What are the critical illnesses covered by Prime Care E-Commerce?', N'<p>There are 35 critical illnesses covered:</p>
<ul class="decimal-list">
<li>Cancer</li>
<li>Heart Attack (Myocardial Infarction)</li>
<li>Stroke</li>
<li>Coronary Artery By-pass Graft</li>
<li>Kidney Failure</li>
<li>Aplastic Anemia</li>
<li>Blindness (Profound Vision Loss)</li>
<li>End Stage Lung Disease</li>
<li>Coma</li>
<li>Deafness (Loss of Hearing)</li>
<li>Heart Valve Surgery</li>
<li>Loss of Speech</li>
<li>Major Burns (Third-Degree Burns)</li>
<li>Major Organ / Bone Marrow Transplantation</li>
<li>Multiple Sclerosis</li>
<li>Muscular Dystrophy</li>
<li>Paralysis / Paraplegia</li>
<li>Parkinson’s Disease</li>
<li>Surgery to Aorta</li>
<li>Alzheimer''s Disease / Severe Dementia</li>
<li>Motor Neuron Disease</li>
<li>Benign Brain Tumor</li>
<li>Persistent Vegetative State (aka Loss of Independent Existence)</li>
<li>Poliomyelitis</li>
<li>Bacterial Meningitis</li>
<li>Terminal Illness</li>
<li>Primary Cardiomyopathy </li>
<li>Brain Surgery</li>
<li>Fulminant Hepatitis</li>
<li>End Stage Liver Failure </li>
<li>Primary Pulmonary Arterial Hypertension</li>
<li>HIV Infection caught at work in an Eligible Occupation</li>
<li>Major Head Trauma</li>
<li>Loss of Limbs</li>
<li>HIV due to Blood Transfusion</li>
</ul>', 1, 0, 33)
GO
SET IDENTITY_INSERT [dbo].[TblFaqs] OFF
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_FaqCategory]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TblFaqCategories] ADD  CONSTRAINT [DF_TblFaqCategories_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO
