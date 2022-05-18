using DotaWin.DataAPI;
using Microsoft.EntityFrameworkCore;
using DotaWin.DataAPI.Utilities;

// Connect to DB
var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DotaWinDatabase");
builder.Services.AddDbContext<DotaWinDbContext>(opt => opt.UseNpgsql(connString));

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Migrate DB updates if needed
app.RunDatabaseMigrations<DotaWinDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
