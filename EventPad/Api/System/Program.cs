using EventPad.Common;
using EventPad.Api;
using EventPad.Api.Configuration;
using EventPad.Api.Context;
using EventPad.Logger;
using EventPad.Settings;


var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");


builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppCors();

services.AddAppControllerAndViews();

services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSettings);

services.RegisterServices(builder.Configuration);

services.AddAppAutoMappers();

services.AddAppValidator();


var app = builder.Build();

var Logger = app.Services.GetRequiredService<IAppLogger>();

app.UseAppSwagger();

app.UseAppHealthChecks();
app.UseAppCors();
app.UseAppControllerAndViews();

app.UseAppMiddlewares();


DbInitializer.Execute(app.Services);

//DbSeeder.Execute(app.Services);


Logger.Information("The EventPad API was started");

app.Run();

Logger.Information("The EventPad API was stopped");
