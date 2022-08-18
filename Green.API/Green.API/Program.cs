using Green.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
string connectionString = File.ReadAllText("/Users/brandon/connection.txt");
    //builder.Configuration.GetConnectionString("dbURL");

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
    builder.AllowAnyHeader();
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();