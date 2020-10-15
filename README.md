# InLife Store

## Deployment Guide



### Preparation

1. Download and install NodeJS (needed by Angular project) from <a href="https://nodejs.org/" target="_blank">**https://nodejs.org/**</a>

2. Download and install .NET Core SDK (needed by the API project) from <a href="https://dotnet.microsoft.com/download" target="_blank">**https://dotnet.microsoft.com/download**</a>

3. Download and install Visual Studio 2019 (needed for comparing and updating the database schema) from  <a href="https://visualstudio.microsoft.com/downloads" target="_blank">**https://visualstudio.microsoft.com/downloads/**</a>



### StoreFront (Angular)

1. Open a terminal window

2. Go to `InLife.Store.StoreFront.Old` folder. Ignore `InLife.Store.StoreFront`, it is still under development.

3. Install Angular CLI (if not yet installed)
```shell
$ npm install -g @angular/cli
```

4. Install/Update the project NPM packages
```shell
$ npm install
```

5. Publish the source code
```shell
// For Projectgrey DEV Environment
$ ng build --configuration dev

// For InLife UAT Environment
$ ng build --configuration uat

// For InLife PROD Environment
$ ng build --prod
```

6. Copy the files from the published folder `InLife.Store.Web/dist` to the designated Azure App Service



### Identity Service (ASP.NET Core)

1. Open a terminal window

2. Go to `InLife.Store.Identity` folder

3. Restore reference packages and publish the source code
```shell
$ dotnet restore
$ dotnet build
$ dotnet publish -c Release
```

4. Copy the files from the published folder `InLife.Store.Identity/bin/Release/netcoreapp3.1/publish` to the designated Azure App Service



### API (ASP.NET Core)

1. Open a terminal window

2. Go to `InLife.Store.Api` folder

3. Restore reference packages and publish the source code
```shell
$ dotnet restore
$ dotnet build
$ dotnet publish -c Release
```

4. Copy the files from the published folder `InLife.Store.Api/bin/Release/netcoreapp3.1/publish` to the designated Azure App Service



### Content Management System (ASP.NET Core)

1. Open a terminal window

2. Go to `InLife.Store.Cms` folder

3. Restore reference packages and publish the source code
```shell
$ dotnet restore
$ dotnet build
$ dotnet publish -c Release
```

4. Copy the files from the published folder `InLife.Store.Cms/bin/Release/netcoreapp3.1/publish` to the designated Azure App Service



### Database

1. Launch Visual Studio, and in the menu, click **Open** > **File**

2. Go to `InLife.Store.Database` folder

3. Open the `InLife.Store.Database.sqlproj` project file

4. In the **Solution Explorer**, right-click the `InLife.Store.Database` project then click on **Schema Compare**

5. In. the toolbar, select the database project as the source, and select the Azure Database as the target

6. Click on the **Compare** button and check to see if there are conflicts

7. Click on the **Update** button to update the target database



---



## Setup and Configuration Guide



### StoreFront URL Settings

1. Go to the `InLife.Store.StoreFront` source code folder.

2. Go to the `/src/environments` source code folder.

3. Open `environment.prod.ts` in your text editor.

4. Change thhe URLs based on your environment setup.
```javascript
appApi:
{
    host: 'https://host/api',
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

5. Save the file then rebuild the application



### StoreFront HTML Base URL Settings

1. Go to the `InLife.Store.StoreFront` source code folder.

2. Open the following files:
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

3. Replace the URL in the HTML base tag according to your server URL:
```html
<base href="https://host/">
```

4. Save the changes then rebuild the application



### ASP.NET Core Database Connection Strings (`InLife.Store.Identity`, `InLife.Store.Api`, `InLife.Store.Cms`)

1. Go to the project's published folder.

2. Open `appsettings.json` file in your text editor.

3. Go to the `DefaultConnection` under `ConnectionStrings` section and replace the connection string provided by your Azure Database Server
```json
"ConnectionStrings":
{
	"DefaultConnection": "Server=tcp:projectgrey.database.windows.net,1433;Initial Catalog=InLife.Store;Persist Security Info=False;User ID=DATABASE_USER;Password=DATABASE_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

4. Save the file then restart the App Service.



### ASP.NET Core SMTP Settings (`InLife.Store.Identity`, `InLife.Store.Api`, `InLife.Store.Cms`)

1. Go to the project's published folder.

2. Open `appsettings.json` file in your text editor.

3. Go to the `Smtp` section and replace the credentials with your own.
```json
"Smtp":
{
	"Host": "my-smtp.host.com",
	"Port": 587,
	"Username": "(づ ￣ ³￣)づ",
	"Password": "ಠ_ಠ",
	"EnableSsl": true
}
```

4. Save the file then restart the App Service.



### Identity Service Settings

1. Go to the `InLife.Store.Identity` published folder.

2. Open `appsettings.json` file in your text editor.

3. Go to the `Clients` section, look for the `inlife.store.cms` client and change the `RedirectUris` and `PostLogoutRedirectUris` value depending on the host configuration of the application.
```json
"Clients":
[
	{
		"ClientId": "inlife.store.cms",

		...

		// replace https://host/cms/ with the URI of your CMS

		// redirect after login
		"RedirectUris": [ "https://host/cms/signin-oidc" ],
		// redirect after logout
		"PostLogoutRedirectUris": [ "https://host/cms/signout-callback-oidc" ],
		// logout uri
		"FrontChannelLogoutUri": "https://host/cms/signout-oidc"
	}
]
```
5. Save the file then restart the App Service.



### CMS Authentication Settings

1. Go to the `InLife.Store.Identity` published folder.

2. Open `appsettings.json` file in your text editor.

3. Go to the `Authentication` section and change the `Authority` value depending on the host configuration of the Identity Service
```json
"Authentication":
{
	"Authority": "https://host/identity",
	"ClientId": "inlife.store.cms"
}
```
4. Save the file then restart the App Service.



### API CORS Whitelisting

1. Go to the `InLife.Store.Api` published folder.

2. Open `appsettings.json` file in your text editor.

3. Go to the `AllowedOrigins` section and add the list of domains you want to have access to the API
```json
"AllowedOrigins":
[
	"https://host"
]
```

4. Save the file then restart the App Service.