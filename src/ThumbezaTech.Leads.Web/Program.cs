using Serilog;

using ThumbezaTech.Leads.Application;
using ThumbezaTech.Leads.Infrastructure.Data;
using ThumbezaTech.Leads.Web.GraphQL;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddApplication();
builder.Services.AddGraphQL();
builder.Services.AddDatabaseInfrastructure(builder.Configuration.GetSection("Neo4J"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();

app.UseGraphQLResolver(app.Environment);

app.Run();

