using Green.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD
string connectionString = SQLCONNSTR_dbURL 
// string connectionString = File.ReadAllText("C:/Users/14845/Desktop/project2login.txt");
=======
string connectionString = SQLCONNSTR_dbURL; 
    //File.ReadAllText("C:/Users/brand/connection.txt");
>>>>>>> origin/daniel

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository>(sp => new SqlRepository(connectionString, sp.GetRequiredService<ILogger<SqlRepository>>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();