using MudBlazor.Services;

using Serilog;

using ThumbezaTech.Leads.Web.Server.Features.Orders;
using ThumbezaTech.Leads.Web.Server.Features.Products;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, config) =>
config.ReadFrom.Configuration(context.Configuration));

Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-ZA");

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(o => o.MaximumReceiveMessageSize = 10 * 1024 * 1024);
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(typeof(Program).Assembly);
  config.Lifetime = ServiceLifetime.Scoped;
});

builder.Services.AddMudServices();

builder.Services.AddLeadsGraphQLClient(builder.Configuration.GetSection("LeadsGraphQLClient"));

builder.Services.AddScoped<ProductState>();
builder.Services.AddScoped<OrderState>();


builder.Services.AddLocalization();
var supportedCultures = new[]
{
    new System.Globalization.CultureInfo("en-ZA"),
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
  options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-ZA");
  options.SupportedCultures = supportedCultures;
  options.SupportedUICultures = supportedCultures;
});

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
//app.UseAuthorization();
//app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
