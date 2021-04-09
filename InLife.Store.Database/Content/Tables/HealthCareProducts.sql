﻿CREATE TABLE [Content].[HealthCareProducts] (
    [Id]                                  INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]                         DATETIMEOFFSET (7) NOT NULL,
    [CreatedBy]                           VARCHAR (36)       NULL,
    [UpdatedDate]                         DATETIMEOFFSET (7) NULL,
    [UpdatedBy]                           VARCHAR (36)       NULL,
    [ProductImg]                          NVARCHAR (MAX)     NOT NULL,
    [ProductName]                         NVARCHAR (200)     NOT NULL,
    [ProductPrice]                        NVARCHAR (100)     NOT NULL,
    [ProductCode]                         NVARCHAR (100)     NULL,
    [ShortDescription]                    NVARCHAR (1000)    NULL,
    [PriceWithOffer]                      NVARCHAR (100)     NULL,
    [SortNum]                             INT                NULL,
    [CasesCovered]                        NVARCHAR (500)     NULL,
    [BenefitType]                         NVARCHAR (500)     NULL,
    [AgeEligibility]                      NVARCHAR (500)     NULL,
    [NumberOfAvailments]                  NVARCHAR (500)     NULL,
    [BenefitLimit]                        NVARCHAR (500)     NULL,
    [DocProFee]                           NVARCHAR (500)     NULL,
    [RoomAccommodation]                   NVARCHAR (500)     NULL,
    [LaboratoryDiagnosticPro]             NVARCHAR (500)     NULL,
    [MedicinesAsMedicallyNeeded]          NVARCHAR (500)     NULL,
    [UseOfOperationRoom]                  NVARCHAR (500)     NULL,
    [SurgerySurgonFees]                   NVARCHAR (500)     NULL,
    [Laparoscopic]                        NVARCHAR (500)     NULL,
    [MRA]                                 NVARCHAR (500)     NULL,
    [MRI]                                 NVARCHAR (500)     NULL,
    [CT]                                  NVARCHAR (500)     NULL,
    [Therapetic]                          NVARCHAR (500)     NULL,
    [PainManagement]                      NVARCHAR (500)     NULL,
    [Arthoscopic]                         NVARCHAR (500)     NULL,
    [OtherMedical]                        NVARCHAR (500)     NULL,
    [OneTime]                             NVARCHAR (500)     NULL,
    [Usage]                               NVARCHAR (500)     NULL,
    [AccreditedHospitals]                 NVARCHAR (500)     NULL,
    [MER]                                 NVARCHAR (500)     NULL,
    [AFR]                                 NVARCHAR (500)     NULL,
    [ARP]                                 NVARCHAR (500)     NULL,
    [Validity]                            NVARCHAR (500)     NULL,
    [Waiting]                             NVARCHAR (500)     NULL,
    [NumberOfRegistrations]               NVARCHAR (500)     NULL,
    [UnlimitedTeleMed]                    NVARCHAR (500)     NULL,
    [PreExistingConCover]                 NVARCHAR (500)     NULL,
    [NonAccreditedHospitals]              NVARCHAR (500)     NULL,
    [ReimbursementNonAccreditedHospitals] NVARCHAR (500)     NULL,
    [TopSixHospitalAccess]                NVARCHAR (500)     NULL,
    [RegistrationOfSucceedingVouchers]    NVARCHAR (500)     NULL,
    [Combinability]                       NVARCHAR (500)     NULL,
    [IndividualOrGroup]                   NVARCHAR (500)     NULL,
    [PrepaidPlan]                         NVARCHAR (500)     NULL,
    [Consultation]                        NVARCHAR (500)     NULL,
    [Inclusions]                          NVARCHAR (MAX)     NULL,
    [SpecialModalities]                   NVARCHAR (500)     NULL,
    [Exclusions]                          NVARCHAR (MAX)     NULL,
    [FTFConsultation]                     NVARCHAR (500)     NULL,
    [Telemedicine]                        NVARCHAR (500)     NULL,
    [DentalConsultation]                  NVARCHAR (500)     NULL,
    [DentalServicesBenefit]               NVARCHAR (500)     NULL,
    [HospitalNetwork]                     NVARCHAR (500)     NULL,
    [RegistrationRules]                   NVARCHAR (500)     NULL,
    [MedicalCoverage]                     NVARCHAR (500)     NULL,
    [LearnMoreBtnLink]                    NVARCHAR (500)     NULL,
    [BuyNowBtnLink]                       NVARCHAR (500)     NULL,
    [Coverage]                            NVARCHAR (500)     NULL,
    [VoucherUsed]                         NVARCHAR (500)     NULL,
    [VoucherUnused]                       NVARCHAR (500)     NULL,
    [ConsultationCards]                   NVARCHAR (500)     NULL,
    [InPatient]                           NVARCHAR (500)     NULL,
    [OutPatient]                          NVARCHAR (500)     NULL,
    CONSTRAINT [PK_Content_HealthCareProducts] PRIMARY KEY CLUSTERED ([Id] ASC)
);
