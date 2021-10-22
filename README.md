# Read-CosmosDbKeys-Using-ManagedIdentities
<h1> System Assigned Managed Identities </h2>

shows how to  read the cosmosdb account keys at runtime using managed identities rather than saving the keys in a store or vault.

In Program.cs we read the cosmos db keys using managed identities (Ln: 54) using the <strong>  AzureServiceTokenProvider </strong>  by calling the list keys API. <br>
Checkout the <strong> GetCosmosKeys()<br>

Then we register the keys to the DI container (ln: 56) <br>
<code> services.AddScoped<DatabaseAccountListKeysResult>(svc => keys); </code><br>

Once registered in the DI container , we inject them into the WeatherForecastController and retrieve the keys in the GET method (Ln: 17).


<h1> User assigned managed Identites </h1>
 In case of user assigned managed identities, we need to pass a connection string to the <strong>AzureServiceTokenProvider</strong> constructor<br>
<code> var azureServiceTokenProvider = new AzureServiceTokenProvider("RunAs=App;AppId={ClientId of user-assigned identity}"); </code><br>

Rest of the code remains the same

<h2>NOTE</h2>
<h3> Microsoft.Azure.Services.AppAuthentication is no longer recommended to use with new Azure SDK. It is replaced with new Azure Identity client library available for .NET, Java, TypeScript and Python and should be used for all new development. Information about how to migrate to Azure Identitycan be found here: AppAuthentication to Azure.Identity Migration Guidance. </h3>
<br>

<h2> References </h2>
<strong> 1.  https://docs.microsoft.com/en-us/azure/cosmos-db/managed-identity-based-authentication </strong><br>
<strong> 2.  https://docs.microsoft.com/en-us/dotnet/api/overview/azure/service-to-service-authentication </strong><br>
<strong> 3.  https://docs.microsoft.com/en-us/dotnet/api/overview/azure/app-auth-migration </strong><br>



