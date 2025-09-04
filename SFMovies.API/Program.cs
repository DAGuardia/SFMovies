using SFMovies.Application.Interfaces;
using SFMovies.Application.Services;
using SFMovies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var allowedOrigin = builder.Configuration["Cors:AllowedOrigin"];
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
{
    p.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod();
}));
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
