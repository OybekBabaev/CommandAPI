using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionStringBuilder = new NpgsqlConnectionStringBuilder();
connectionStringBuilder.ConnectionString = 
    builder.Configuration.GetConnectionString("PostgreSqlConnection");
connectionStringBuilder.Username = builder.Configuration["UserID"];
connectionStringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(options =>
    options.UseNpgsql(connectionStringBuilder.ConnectionString));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.MapGet("/", async ctx => await ctx.Response.WriteAsync("Hallo Welt!"));
app.MapControllers();

app.Run();