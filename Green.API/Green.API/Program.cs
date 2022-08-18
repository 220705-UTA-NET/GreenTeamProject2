using Green.Api.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("dbURL");
//File.ReadAllText("/Users/brandon/connection.txt");

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository>(sp => new SqlRepository(connectionString, sp.GetRequiredService<ILogger<SqlRepository>>()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();