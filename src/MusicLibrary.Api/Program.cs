using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var connectionString =
    builder.Configuration.GetConnectionString("MusicLibrary");

builder.Services.Configure<NavidromeOptions>(
    builder.Configuration.GetSection("Navidrome")
);
builder.Services.AddHttpClient<NavidromeClient>();

builder.Services.AddSingleton(
   NpgsqlDataSource.Create(connectionString!));

builder.Services.AddSingleton<INpgsqlConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<DatabaseInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.ConfigureApi();
app.Run();
