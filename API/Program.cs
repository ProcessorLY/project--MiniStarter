//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using API.Modules;

var builder = WebApplication.CreateBuilder(args).UseSerilog();

// Add services to the container.
builder.Services.AddServices(builder.Configuration);
builder.Services.ModulesInjection(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UsePipelineBuilder(builder.Configuration);
app.Run();
