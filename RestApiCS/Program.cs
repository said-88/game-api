using RestApiCS.Data;
using RestApiCS.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSqlServer<GameStoreContext>(connection);

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndPoints();

await app.MigrateDbAsync();

app.Run();
