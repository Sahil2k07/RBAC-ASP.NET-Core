using rbac_web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFrameworkServices();
builder.Services.AddAppConfigs(builder.Configuration);
builder.Services.AddEnvServices(builder.Environment);
builder.Services.AddAuthServices();
builder.Services.AddAppRepositories();
builder.Services.AddAppServices();

var app = builder.Build();

app.UseAppConfigurations();

app.Run();
