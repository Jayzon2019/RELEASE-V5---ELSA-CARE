# InLife Store

## Deployment Guide



### Preparation

1. Download and install NodeJS (needed by Angular project) from <a href="https://nodejs.org/" target="_blank">**https://nodejs.org/**</a>

2. Download and install .NET Core SDK (needed by the API project) from <a href="https://dotnet.microsoft.com/download" target="_blank">**https://dotnet.microsoft.com/download**</a>

3. Download and install Visual Studio 2019 (needed for comparing and updating the database schema) from  <a href="https://visualstudio.microsoft.com/downloads" target="_blank">**https://visualstudio.microsoft.com/downloads/**</a>



---



### StoreFront (Angular)

1. Open a terminal window

2. Go to `InLife.Store.StoreFront.Old` folder. Ignore `InLife.Store.StoreFront`, it is still under development.

3. Go to the `/src/environments` source code folder.

4. Open `environment.ENV.ts` in your text editor.
```shell
// For InLife UAT Environment
$ environment.uat.ts

// For InLife PROD Environment
$ environment.prod.ts
```

5. Change the URLs based on your environment setup.
```javascript
appApi:
{
    host: 'https://HOST/api',
    quotesEndpoint: '/quotes',
    ordersEndpoint: '/orders',
},

primeCareApi:
{
    subscriptionKey: 'SUBSCRIPTION-KEY',
    host: 'https://apim-uat.insularlife.com.ph/pg/v4',
    createApplicationEndpoint: '/CreateApplication',
    savePaymentEndpoint: '/SavePayment',
},

paymentGatewayEndpoint: 'https://beta2.insularlife.com.ph/CustomerPortal/Customer/E-Payment/ILPay.ashx'
```

5. Save the file

6. Go to the `/src` source code folder.

7. Open the following files in your text editor:
```terminal
index.html
faqs.html
get-prime-care.html
ineligible.html
maintenance.html
prime-care.html
redirect.html
template.html
thankyou.html
```

8. Replace the URL in the HTML base tag according to your server URL:
```html
<base href="https://HOST/">
```

9. Save the changes

10. Go to the `/src/assets/js` source code folder.

11. Open `appsettings.js` in your text editor.

12. Replace the URL of the API
```javascript
var api = "https://HOST/api/";
```

13. Save the file

14. Install Angular CLI (if not yet installed)
```shell
$ npm install -g @angular/cli
```

15. Install/Update the project NPM packages
```shell
$ npm install
```

16. Publish the source code
```shell
// For Projectgrey DEV Environment
$ ng build --configuration dev

// For InLife UAT Environment
$ ng build --configuration uat

// For InLife PROD Environment
$ ng build --prod
```

17. Copy the files from the published folder `/dist` to the designated Azure App Service



---



### Identity Service (ASP.NET Core)

1. Open a terminal window

2. Go to `InLife.Store.Identity` folder

3. Restore reference packages and publish the source code
```shell
$ dotnet restore
$ dotnet build
$ dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```

4. Go to the published folder `/bin/Release/netcoreapp3.1/win-x64/publish`.

5. Open `appsettings.json` in your text editor.

6. Go to the `DefaultConnection` under `ConnectionStrings` section and replace the connection string provided by your Azure Database Server
```json
"ConnectionStrings":
{
	"DefaultConnection": "Server=tcp:projectgrey.database.windows.net,1433;Initial Catalog=InLife.Store;Persist Security Info=False;User ID=DATABASE_USER;Password=DATABASE_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

7. Go to the `CustomDomain` section and replace the values depending on your hosting environment.
```json
"CustomDomain":
{
	"Protocol": "https",
	"Host": "dev-inlifestore.projectgrey.net",
	"PathBase": "/identity"
}
```

8. Go to the `Smtp` section and replace the credentials with your own.
```json
"Smtp":
{
	"Host": "my-smtp.host.com",
	"Port": 587,
	"Username": "USERNAME",
	"Password": "PASSWORD",
	"EnableSsl": true
}
```

9. Go to the `Email` section and replace all values of `SenderEmail` and `Recipients` with your own.

10. Go to the `Clients` section, look for the `inlife.store.cms` client and change the `RedirectUris` and `PostLogoutRedirectUris` value depending on the host configuration of the application.
```json
"Clients":
[
	{
		"ClientId": "inlife.store.cms",

		...

		// redirect after login
		"RedirectUris": [ "https://dev-inlifestore.projectgrey.net/cms/signin-oidc" ],
		// redirect after logout
		"PostLogoutRedirectUris": [ "https://dev-inlifestore.projectgrey.net/cms/signout-callback-oidc" ],
		// logout uri
		"FrontChannelLogoutUri": "https://dev-inlifestore.projectgrey.net/cms/signout-oidc"
	}
]
```
11. Apply the appropriate changes to `inlife.store.ams`

12. Save the file.

13. Open `web.config` in your text editor.

15. Replace `modules="AspNetCoreModuleV2"` with `modules="AspNetCoreModule"`
```xml
<system.webServer>
	<handlers>
		<add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
	</handlers>
	<aspNetCore processPath=".\InLife.Store.Identity.exe" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />
</system.webServer>
```

16. Copy the files from the published folder `/bin/Release/netcoreapp3.1/win-x64/publish` to the designated Azure App Service

17. Restart the App Service



---



### API (ASP.NET Core)

1. Open a terminal window

2. Go to `InLife.Store.Api` folder

3. Restore reference packages and publish the source code
```shell
$ dotnet restore
$ dotnet build
$ dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```

4. Go to the published folder `/bin/Release/netcoreapp3.1/win-x64/publish`.

5. Open `appsettings.json` in your text editor.

6. Go to the `DefaultConnection` under `ConnectionStrings` section and replace the connection string provided by your Azure Database Server
```json
"ConnectionStrings":
{
	"DefaultConnection": "Server=tcp:projectgrey.database.windows.net,1433;Initial Catalog=InLife.Store;Persist Security Info=False;User ID=DATABASE_USER;Password=DATABASE_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

7. Go to the `CustomDomain` section and replace the values depending on your hosting environment.
```json
"CustomDomain":
{
	"Protocol": "https",
	"Host": "dev-inlifestore.projectgrey.net",
	"PathBase": "/api"
}
```

8. Go to the `Smtp` section and replace the credentials with your own.
```json
"Smtp":
{
	"Host": "my-smtp.host.com",
	"Port": 587,
	"Username": "USERNAME",
	"Password": "PASSWORD",
	"EnableSsl": true
}
```

9. Go to the `Email` section and replace all values of `SenderEmail` and `Recipients` with your own.

10. Go to the `AllowedOrigins` section and add the list of domains you want to have access to the API
```json
"AllowedOrigins":
[
	"https://dev-inlifestore.projectgrey.net"
]
```

11. Save the file.

12. Open `web.config` in your text editor.

13. Replace `modules="AspNetCoreModuleV2"` with `modules="AspNetCoreModule"`
```xml
<system.webServer>
	<handlers>
		<add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
	</handlers>
	<aspNetCore processPath=".\InLife.Store.Api.exe" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />
</system.webServer>
```

14. Copy the files from the published folder `/bin/Release/netcoreapp3.1/win-x64/publish` to the designated Azure App Service

15. Restart the App Service



---



### Content Management System (ASP.NET Core)

1. Open a terminal window

2. Go to `InLife.Store.Cms` folder

3. Restore reference packages and publish the source code
```shell
$ dotnet restore
$ dotnet build
$ dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```

4. Go to the published folder `/bin/Release/netcoreapp3.1/win-x64/publish`.

5. Open `appsettings.json` in your text editor.

6. Go to the `DefaultConnection` under `ConnectionStrings` section and replace the connection string provided by your Azure Database Server
```json
"ConnectionStrings":
{
	"DefaultConnection": "Server=tcp:projectgrey.database.windows.net,1433;Initial Catalog=InLife.Store;Persist Security Info=False;User ID=DATABASE_USER;Password=DATABASE_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

7. Go to the `CustomDomain` section and replace the values depending on your hosting environment.
```json
"CustomDomain":
{
	"Protocol": "https",
	"Host": "dev-inlifestore.projectgrey.net",
	"PathBase": "/cms"
}
```

8. Go to the `Smtp` section and replace the credentials with your own.
```json
"Smtp":
{
	"Host": "my-smtp.host.com",
	"Port": 587,
	"Username": "USERNAME",
	"Password": "PASSWORD",
	"EnableSsl": true
}
```

9. Go to the `Email` section and replace all values of `SenderEmail` and `Recipients` with your own.

10. Go to the `Authentication` section and change the `Authority` value depending on the host configuration of the Identity Service
```json
"Authentication":
{
	"Authority": "https://dev-inlifestore.projectgrey.net/identity",
	"ClientId": "inlife.store.cms"
}
```

11. Save the file.

12. Open `web.config` in your text editor.

13. Replace `modules="AspNetCoreModuleV2"` with `modules="AspNetCoreModule"`
```xml
<system.webServer>
	<handlers>
		<add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
	</handlers>
	<aspNetCore processPath=".\InLife.Store.Cms.exe" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />
</system.webServer>
```

14. Copy the files from the published folder `/bin/Release/netcoreapp3.1/win-x64/publish` to the designated Azure App Service

15. Restart the App Service



---



### Affiliate Marketing System (ASP.NET Core)

*UNDER DEVELOPMENT*



