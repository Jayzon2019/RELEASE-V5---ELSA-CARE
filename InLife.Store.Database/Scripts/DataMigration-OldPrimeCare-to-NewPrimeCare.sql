--------------------------------------------------------------------------------------------
--	Migrate Prime Care Applications
--------------------------------------------------------------------------------------------

merge [PrimeCare].[Applications] t
using [dbo].[Quotes] s
on
(
	cast(convert(binary(16), reverse(convert(binary(16), 1000000000000000 + s.[Id]))) as uniqueidentifier) = t.[Id]
)

when matched
    then update set
		t.[Status]           = 'Quote',
		t.[ProductName]      = s.[ProductName],
		t.[ProductCode]      = s.[ProductCode],
		t.[PlanCode]         = '-',
		t.[PlanName]         = '-',
		t.[PaymentFrequency] = case when s.[PaymentMode] = 1 then 'Monthly' else 'Annual' end,
		t.[PlanFaceAmount]   = s.[ProductFaceAmount],
		t.[PlanPremium]      = 0,
		t.[AgentCode]        = s.[AgentCode],
		t.[AgentFirstName]   = s.[AgentFirstName],
		t.[AgentLastName]    = s.[AgentLastName],
		t.[ReferralSource]   = s.[ReferralSource],
		t.[Health1]          = s.[Health1],
		t.[Health2]          = s.[Health2],
		t.[Health3]          = s.[Health3],
		t.[CustomerId]       = cast(convert(binary(16), reverse(convert(binary(16), 2000000000000000 + s.[CustomerId]))) as uniqueidentifier),
		t.[IsEligible]       = s.[IsEligible]
when not matched
    then insert
	(
		[Id],
		[CreatedDate],
		[ReferenceCode],
		[Status],
		[ProductName],
		[ProductCode],
		[PlanCode],
		[PlanName],
		[PaymentFrequency],
		[PlanFaceAmount],
		[PlanPremium],
		[AgentCode],
		[AgentFirstName],
		[AgentLastName],
		[ReferralSource],
		[Health1],
		[Health2],
		[Health3],
		[CustomerId],
		[IsEligible]
	)
	values
	(
		cast(convert(binary(16), reverse(convert(binary(16), 1000000000000000 + s.[Id]))) as uniqueidentifier),
		s.[DateCreated],
		('OLD-' + right('00000000'+ rtrim(s.[Id]), 8)),
		'Quote',
		s.[ProductName],
		s.[ProductCode],
		'-',
		'-',
		(case when s.[PaymentMode] = 1 then 'Monthly' else 'Annual' end),
		s.[ProductFaceAmount],
		0,
		s.[AgentCode],
		s.[AgentFirstName],
		s.[AgentLastName],
		s.[ReferralSource],
		s.[Health1],
		s.[Health2],
		s.[Health3],
		cast(convert(binary(16), reverse(convert(binary(16), 2000000000000000 + s.[CustomerId]))) as uniqueidentifier),
		s.[IsEligible]
	)
;

--select * from [PrimeCare].[Applications];
--select * from [dbo].[Quotes];



--------------------------------------------------------------------------------------------
--	Migrate Prime Care Addresses 
--------------------------------------------------------------------------------------------

merge [PrimeCare].[Addresses] t
using [dbo].[Addresses] s
on
(
	cast(convert(binary(16), reverse(convert(binary(16), 3000000000000000 + s.[Id]))) as uniqueidentifier) = t.[Id]
)

when matched
    then update set
		t.[CreatedDate] = s.[DateCreated],
		t.[PhoneNumber] = s.[PhoneNumber],
		t.[Address1]    = s.[Address1],
		t.[Address2]    = s.[Address2],
		t.[Address3]    = s.[Address3],
		t.[City]        = s.[City],
		t.[Region]      = s.[Region],
		t.[Country]     = s.[Country],
		t.[ZipCode]     = s.[ZipCode]
when not matched
    then insert
	(
		[Id],
		[CreatedDate],
		[PhoneNumber],
		[Address1],
		[Address2],
		[Address3],
		[City],
		[Region],
		[Country],
		[ZipCode]
	)
	values
	(
		cast(convert(binary(16), reverse(convert(binary(16), 3000000000000000 + s.[Id]))) as uniqueidentifier),
		s.[DateCreated],
		s.[PhoneNumber],
		s.[Address1],
		s.[Address2],
		s.[Address3],
		s.[City],
		s.[Region],
		s.[Country],
		s.[ZipCode]
	)
;

--select * from [dbo].[Addresses];
--select * from [PrimeCare].[Addresses];



--------------------------------------------------------------------------------------------
--	Migrate Persons 
--------------------------------------------------------------------------------------------

merge [PrimeCare].[Persons] t
using [dbo].[Customers] s
on
(
	cast(convert(binary(16), reverse(convert(binary(16), 2000000000000000 + s.[Id]))) as uniqueidentifier) = t.[Id]
)

when matched
    then update set
		t.[CreatedDate]   = s.[DateCreated],
		t.[NamePrefix]    = s.[NamePrefix],
		t.[NameSuffix]    = s.[NameSuffix],
		t.[FirstName]     = s.[FirstName],
		t.[MiddleName]    = s.[MiddleName],
		t.[LastName]      = s.[LastName],
		t.[Gender]        = s.[Gender],
		t.[BirthDate]     = s.[BirthDate],
		t.[EmailAddress]  = s.[EmailAddress],
		t.[MobileNumber]  = s.[MobileNumber],
		t.[HomeAddressId] = cast(convert(binary(16), reverse(convert(binary(16), 3000000000000000 + s.[HomeAddressId]))) as uniqueidentifier)
		
when not matched
    then insert
	(
		[Id],
		[CreatedDate],
		[NamePrefix],
		[NameSuffix],
		[FirstName],
		[MiddleName],
		[LastName],
		[Gender],
		[BirthDate],
		[EmailAddress],
		[MobileNumber],
		[HomeAddressId]
	)
	values
	(
		cast(convert(binary(16), reverse(convert(binary(16), 2000000000000000 + s.[Id]))) as uniqueidentifier),
		s.[DateCreated],
		s.[NamePrefix],
		s.[NameSuffix],
		s.[FirstName],
		s.[MiddleName],
		s.[LastName],
		s.[Gender],
		s.[BirthDate],
		s.[EmailAddress],
		s.[MobileNumber],
		cast(convert(binary(16), reverse(convert(binary(16), 3000000000000000 + s.[HomeAddressId]))) as uniqueidentifier)
	)
;

--select * from [dbo].[Customers];
--select * from [PrimeCare].[Persons];
