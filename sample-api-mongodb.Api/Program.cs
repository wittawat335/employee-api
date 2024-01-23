using sample_api_mongodb.Core.Interfaces;
using sample_api_mongodb.Core.Services;
using sample_api_mongodb.Infrastructure;
using sample_api_mongodb.Core;
;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectInfraConfig(builder.Configuration);
builder.Services.InjectJWTConfig(builder.Configuration);
builder.Services.InjectServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
