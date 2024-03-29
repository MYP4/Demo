using EventPad.Common;
using EventPad.Logger;
using EventPad.Settings;
using EventPad.Worker;
using EventPad.Worker.Configuration;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
//var emailSettings = Settings.Load<EmailSettings>("Email");


builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppHealthChecks();

services.RegisterServices(builder.Configuration);



var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();

app.UseAppHealthChecks();




//DbSeeder.Execute(app.Services);

logger.Information("The Worker API was started");


logger.Information("Try to connect to RabbitMq");

app.Services.GetRequiredService<ITaskExecutor>().Start();

logger.Information("RabbitMq connected");

app.Run();

logger.Information("The Worker API was stopped");
