using System.Text.Json.Serialization;
using ServiTec.Backend.Extensions;
using ServiTec.Backend.Middlewares;
using ServiTec.Infrastructure;
using ServiTec.Infrastructure.Persistence;

DotNetEnv.Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddFrontendCors(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await DbInitializer.InitializeAsync(app.Services);
}

// Pipeline
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(CorsExtensions.FrontendPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
