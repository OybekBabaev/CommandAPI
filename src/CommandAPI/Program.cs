using CommandAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();

var app = builder.Build();

app.MapGet("/", async ctx => await ctx.Response.WriteAsync("Hallo Welt!"));
app.MapControllers();

app.Run();