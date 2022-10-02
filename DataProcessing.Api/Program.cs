using System.Text.Json;
using System.Text.Json.Serialization;
using DataProcessing.Api;
using DataProcessing.Api.Middleware;
using DataProcessing.Application;
using DataProcessing.Application.Models;
using DataProcessing.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVICE REGISTRATION
// add custom services
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddApiServices();

// add framework services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // convert enum numbers to strings in Swagger
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Data Processing API", Version = "v1" });
});
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add(nameof(DataJobDto), typeof(DataJobDto));
});

// 2. BUILD APP
var app = builder.Build();

// 3. CONFIGURE MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
