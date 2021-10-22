using Microsoft.Azure.Services.AppAuthentication;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//add cosmos db keys to the container
await GetCosmosKeys(builder.Services);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CosmosManagedIdentities", Version = "v1" });
});



var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosmosManagedIdentities v1"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


async Task GetCosmosKeys(IServiceCollection services)
{
    string subscriptionId = "";
    string resourceGroupName = "";
    string cosmosAccountName = "";

    HttpClient httpClient = new HttpClient();
    var azureServiceTokenProvider = new AzureServiceTokenProvider();

    // Authenticate to the Azure Resource Manager to get the Service Managed token.
    string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://management.azure.com/", tenantId: "myazure tenantid");

    // Setup the List Keys API to get the Azure Cosmos DB keys.
    string endpoint = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.DocumentDB/databaseAccounts/{cosmosAccountName}/listKeys?api-version=2019-12-12";

    // Add the access token to request headers.
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    // Post to the endpoint to get the keys result.
    var result = await httpClient.PostAsync(endpoint, new StringContent(""));

    // Get the result back as a DatabaseAccountListKeysResult.
    var res = await result.Content.ReadAsStringAsync();
    DatabaseAccountListKeysResult keys = await result.Content.ReadFromJsonAsync<DatabaseAccountListKeysResult>();

    services.AddScoped<DatabaseAccountListKeysResult>(svc => keys);

}
