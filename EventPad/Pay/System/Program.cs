using EventPad.Common;
using EventPad.Logger;
using EventPad.Settings;
using EventPad.Pay;
using EventPad.Pay.Configuration;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Seeder;


var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
//var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");


builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.RegisterServices(builder.Configuration);

services.AddAppAutoMappers();

services.AddAppValidator();


var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();



app.UseAppHealthChecks();

app.UseAppMiddlewares();


DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);

logger.Information("The PayMS API was started");


logger.Information("Try to connect to RabbitMq");

app.Services.GetRequiredService<ITaskExecutor>().Start();

logger.Information("RabbitMq connected");

app.Run();

logger.Information("The PayMS API was stopped");
