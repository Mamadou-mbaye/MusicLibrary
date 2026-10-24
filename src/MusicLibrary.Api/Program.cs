using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = 
builder.Configuration.GetConnectionString("MusicLibrary");
builder.Services.Configure<NavidromeOptions>(
    builder.Configuration.GetSection("Navidrome"));

builder.Services.AddHttpClient<NavidromeClient>();
builder.Services.AddScoped<INavidromeClient>(sp => sp.GetRequiredService<NavidromeClient>());

builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString!));
builder.Services.AddSingleton<INpgsqlConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<DatabaseInitializer>();

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ITrackRepository, TrackRepository>();

builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ITrackService, TrackService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.ConfigureApi();
app.Run();
