using System.Data;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using StargazerTest.ApiService.GraphQL.Queries;
using StargazerTest.Web;
using StargazerTest.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddRedisOutputCache("rediscache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IDbConnection>(x => new ClickHouseConnection("Host=localhost;User=hymares;Port=8123;Password=;Database=default;ReadWriteTimeout=10000;CommandTimeout=10"));

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://apiservice");
});

    
var app = builder.Build();

app.UseOutputCache();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();