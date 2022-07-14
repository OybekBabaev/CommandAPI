var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", async ctx => await ctx.Response.WriteAsync("Hallo Welt!"));
app.MapControllers();

app.Run();