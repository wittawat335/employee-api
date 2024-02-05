using sample_api_mongodb.Infrastructure;
using sample_api_mongodb.Core;
using sample_api_mongodb.Core.Commons;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using sample_api_mongodb.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);
//var services = builder.Services;
var clientUrl = builder.Configuration[Constants.AppSettings.Client_URL]?.ToString();
var cors = builder.Configuration[Constants.AppSettings.CorsPolicy]?.ToString();

builder.Services.InjectInfraConfig(builder.Configuration);
builder.Services.InjectJWTConfig(builder.Configuration);
builder.Services.InjectServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy(cors!, builder =>
    {
        builder.WithOrigins(clientUrl!).AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme 
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddExceptionHandler<GeneralExceptionHandler>();
builder.Services.AddExceptionHandler<AppExceptionHandler>();

var app = builder.Build();
app.UseExceptionHandler(_ => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors!);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
