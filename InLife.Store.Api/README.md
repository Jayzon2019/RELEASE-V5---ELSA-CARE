# InLife Store API Documentation



## Content

Documentation not available



## PrimeCare



### POST /api/prime-care/applications
Create a new application. Use this when saving a quote.

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| planCode*         | string(10)  | |
| planVariantCode*  | string(50)  | |
| planFaceAmount*   | decimal     | |
| planPremium*      | decimal     | |
| paymentFrequency* | string(20)  | Monthly / Annual |
| namePrefix*       | string(10)  | |
| nameSuffix*       | string(10)  | |
| firstName*        | string(50)  | |
| middleName*       | string(50)  | |
| lastName*         | string(50)  | |
| gender*           | string(20)  | Male / Female |
| birthDate*        | date        | YYYY-MM-DD |
| emailAddress*     | string(300) | |
| mobileNumber*     | string(20)  | |
| phoneNumber*      | string(20)  | |
| country*          | string(30)  | |
| region*           | string(30)  | |
| city*             | string(30)  | |
| referralSource*   | string(80)  | |
| agentCode         | string(50)  | |
| agentFirstName    | string(50)  | |
| agentLastName     | string(50)  | |
| health1           | boolean     | |
| health2           | boolean     | |
| health3           | boolean     | |

_* required parameter_
</details>



### POST /api/prime-care/applications/:refcode

Update an application.

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| | | |

_* required parameter_
</details>



### PUT /api/prime-care/applications/:refcode/files/identity-document

Upload an identity document in DataURI PDF format.

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| | | |

_* required parameter_
</details>



### PUT /api/prime-care/applications/:refcode/feedback

NA

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| | | |

_* required parameter_
</details>



### GET /api/prime-care/applications/:refcode/summary

Returns the application summary based on the requested Reference Code `:refcode`

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| | | |

_* required parameter_
</details>



### GET /api/prime-care/applications/:refcode/status

Returns the application status based on the requested Reference Code `:refcode`

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| | | |

_* required parameter_
</details>



## Group



### POST /api/group/applications

Create a new application. Use this when saving a quote.

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| **planCode &bull;**                   | string(20)  | Administrative and Office Based / Security Agencies / Schools and Universities |
| **planVariantCode &bull;**            | string(20)  | Plan 1 / Plan 2 / Plan 3 / Plan 4 |
| **planFaceAmount &bull;**             | decimal     | |
| **planPremium &bull;**                | decimal     | |
| **totalMembers &bull;**               | int         | Required if (Administrative and Office Based / Security Agencies) |
| **totalTeachers &bull;**              | int         | Required if (Schools and Universities) |
| **totalStudents &bull;**              | int         | Required if (Schools and Universities) |
| representativeNamePrefix              | string(10)  | |
| representativeNameSuffix              | string(10)  | |
| **representativeFirstName &bull;**    | string(50)  | |
| representativeMiddleName              | string(50)  | |
| **representativeLastName &bull;**     | string(50)  | |
| representativePhoneNumber             | string(20)  | |
| **representativeMobileNumber &bull;** | string(20)  | |
| **representativeEmailAddress &bull;** | string(300) | |
| **businessStructure &bull;**          | string(30)  | Corporation / Partership / Sole Proprietorship |
| **companyName &bull;**                | string(300) | |
| companyPhoneNumber                    | string(20)  | |
| companyMobileNumber                   | string(20)  | |
| companyEmailAddress                   | string(300) | |
| **companyAddress1 &bull;**            | string(300) | |
| **companyAddress2 &bull;**            | string(300) | |
| **companyTown &bull;**                | string(50)  | |
| **companyCity &bull;**                | string(50)  | |
| **companyRegion &bull;**              | string(50)  | |
| **companyZipCode &bull;**             | string(5)   | |
| companyCountry                        | string(50)  | |

_**&bull;** required parameter_
</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| referenceCode | string(50)  | |
| status        | string(20)  | |
| session       | string(300) | Store the session token on the browser (cookie or session storage), then attach the token as `Session` on the header of every request. |

</details>



### PATCH /api/group/applications/:refcode

Update an existing quote that matches the `refcode`.

<details>
<summary>
    Path parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| refcode | string(50) | The reference code of the application. |

</details>

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| **planCode &bull;**                   | string(20)  | Administrative and Office Based / Security Agencies / Schools and Universities |
| **planVariantCode &bull;**            | string(20)  | Plan 1 / Plan 2 / Plan 3 / Plan 4 |
| **planFaceAmount &bull;**             | decimal     | |
| **planPremium &bull;**                | decimal     | |
| **totalMembers &bull;**               | int         | Required if (Administrative and Office Based / Security Agencies) |
| **totalTeachers &bull;**              | int         | Required if (Schools and Universities) |
| **totalStudents &bull;**              | int         | Required if (Schools and Universities) |
| representativeNamePrefix              | string(10)  | |
| representativeNameSuffix              | string(10)  | |
| **representativeFirstName &bull;**    | string(50)  | |
| representativeMiddleName              | string(50)  | |
| **representativeLastName &bull;**     | string(50)  | |
| representativePhoneNumber             | string(20)  | |
| **representativeMobileNumber &bull;** | string(20)  | |
| **representativeEmailAddress &bull;** | string(300) | |
| **businessStructure &bull;**          | string(30)  | Corporation / Partership / Sole Proprietorship |
| **companyName &bull;**                | string(300) | |
| companyPhoneNumber                    | string(20)  | |
| companyMobileNumber                   | string(20)  | |
| companyEmailAddress                   | string(300) | |
| **companyAddress1 &bull;**            | string(300) | |
| **companyAddress2 &bull;**            | string(300) | |
| **companyTown &bull;**                | string(50)  | |
| **companyCity &bull;**                | string(50)  | |
| **companyRegion &bull;**              | string(50)  | |
| **companyZipCode &bull;**             | string(5)   | |
| companyCountry                        | string(50)  | |

_**&bull;** required parameter_
</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| referenceCode | string(50)  | |
| status        | string(20)  | |

</details>



### PUT /api/group/applications/:refcode

Update an application that matches the `refcode`. Use this to update the application in the master form.

<details>
<summary>
    Path parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| refcode | string(50) | The reference code of the application. |

</details>

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| representativeNamePrefix              | string(10)  | |
| representativeNameSuffix              | string(10)  | |
| **representativeFirstName &bull;**    | string(50)  | |
| representativeMiddleName              | string(50)  | |
| **representativeLastName &bull;**     | string(50)  | |
| representativePhoneNumber             | string(20)  | |
| **representativeMobileNumber &bull;** | string(20)  | |
| **representativeEmailAddress &bull;** | string(300) | |
| **businessStructure &bull;**          | string(30)  | Corporation / Partership / Sole Proprietorship |
| **companyName &bull;**                | string(300) | |
| companyPhoneNumber                    | string(20)  | |
| companyMobileNumber                   | string(20)  | |
| companyEmailAddress                   | string(300) | |
| **companyAddress1 &bull;**            | string(300) | |
| **companyAddress2 &bull;**            | string(300) | |
| **companyTown &bull;**                | string(50)  | |
| **companyCity &bull;**                | string(50)  | |
| **companyRegion &bull;**              | string(50)  | |
| **companyZipCode &bull;**             | string(5)   | |
| companyCountry                        | string(50)  | |

_**&bull;** required parameter_
</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| referenceCode | string(50) | |
| status        | string(20) | Quote / Application |

</details>



### PUT /api/group/applications/:refcode/files/:type

Attach a document to an application with the specified reference code (`:refcode`), and of type (`:type`). Uploaded document should be in PDF format. If the user uploaded an image (JPG or PNG), the image must be converted to PDF in the front-end, and encoded in DataURI format.

Refer to the path parameters table for the document types required by the application.

<details>
<summary>
    Path parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| refcode | string(50) | The reference code of the application. |

| Name | Path Value | Description |  
| :--- | :--- | :---------- |  
| type | employee-census | Employee Census |
| type | admin-form | Entity Plan Admin Form |
| type | representative | ID of the Authorized Representative |
| type | bir-document | BIR Notice of Inclusion in Top 20,000 Corporations and BIR Form 2307 |
| type | business-registration-document | SEC Registration / City / Municipality Ordinance / Congressional Bill / DTI Registration |
| type | incorporation-document | Articles of Incorporation / Partnership and By-Laws |
| type | authorization-document | Secretary’s Certificate / Board Resolution / Partnership Resolution / Managing Partner’s Certificate / Authorization of Owner to the Authorized Representative to transact business with Insular |
| type | individual-applications | Post-policy Issuance: Individual Application Forms |
| type | payment-proof | Bank transaction slip for bank transfers / Official receipt for over-the-counter |

</details>

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| dataUri | string(max) | Encode the file to DataURI before sending to the API |

</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Document Type | Description |  
| :--- | :--- | :--- | :---------- |  
| referenceCode | string(50) | | |
| status  | string(20) | Application Documents | Application / Payment |
| status  | string(20) | Proof of Payment | PaymentProof / Complete |

</details>



### PUT /api/group/applications/:refcode/payment

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| **paymentMethod &bull;**  | string(20) | BankTransfer / OTC |

_**&bull;** required parameter_
</details>



### PUT /api/group/applications/:refcode/feedback

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| **rating &bull;**  | int          | 1 / 2 / 3 / 4 |
| **message &bull;** | string(2000) | |

_**&bull;** required parameter_
</details>



### GET /api/group/applications/:refcode/summary

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| referenceCode              | string(50)  | Administrative and Office Based / Security Agencies / Schools and Universities |
| status                     | string(20)  | Quote / Application / Payment / PaymentProof / Feedback / Complete / Cancelled |
| planCode                   | string(20)  | |
| planVariantCode            | string(20)  | Plan 1 / Plan 2 / Plan 3 / Plan 4 |
| planFaceAmount             | decimal     | |
| planPremium                | decimal     | |
| totalMembers               | int         | |
| totalTeachers              | int         | |
| totalStudents              | int         | |
| businessStructure          | string(30)  | Corporation / Partership / Sole Proprietorship |
| companyName                | string(300) | |
| companyPhoneNumber         | string(20)  | |
| companyMobileNumber        | string(20)  | |
| companyEmailAddress        | string(300) | |
| representativeNamePrefix   | string(10)  | |
| representativeNameSuffix   | string(10)  | |
| representativeFirstName    | string(50)  | |
| representativeMiddleName   | string(50)  | |
| representativeLastName     | string(50)  | |
| representativePhoneNumber  | string(20)  | |
| representativeMobileNumber | string(20)  | |
| representativeEmailAddress | string(300) | |
| companyAddress1            | string(300) | |
| companyAddress2            | string(300) | |
| companyTown                | string(50)  | |
| companyCity                | string(50)  | |
| companyRegion              | string(50)  | |
| companyZipCode             | string(5)   | |
| companyCountry             | string(50)  | |

</details>



### GET /api/group/applications/:refcode/status

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| referenceCode | string(50) | |
| status        | string(20) | Quote / Application / Payment / PaymentProof / Complete / Cancelled |

</details>



### PUT /api/group/applications/:refcode/cancel

<details>
<summary>
    Request headers
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Retrieve this using the OTP flow. |

</details>

<details>
<summary>
    JSON request body parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| **reason &bull;**   | string(2000) | |
| **comments &bull;** | string(2000) | |

_**&bull;** required parameter_
</details>



### PUT /api/group/applications/:refcode/request-otp

Use this on the Reference Code form to return to the application form. After entering the reference code, the user will receive an OTP in their registered email address to be used on the Enter OTP form.



### GET /api/group/applications/:refcode/request-session?otp=[YOUR-OTP]

After entering the OTP, call this endpoint to receive a session token. Store the token on the browser (cookie or session storage), then attach the token as `Session` on the header of every request.

<details>
<summary>
    Query string parameters
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| otp  | string(6) | |

</details>

<details>
<summary>
    JSON response
</summary>

| Name | Type | Description |  
| :--- | :--- | :---------- |  
| session | string(300) | Store the session token on the browser (cookie or session storage), then attach the token as `Session` on the header of every request. |

</details>

## PrimeSecure

Documentation not available
