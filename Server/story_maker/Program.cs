using story_maker;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using story_maker.Services.Implementations;
using story_maker.Services.Interfaces;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(builder.Configuration.GetConnectionString("default"), 
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("default"))));
builder.Services.AddAutoMapper(typeof(Profile));

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICharacterClassService, CharacterClassService>();
builder.Services.AddScoped<ITraitService, TraitService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.UseAuthorization();

app.MapControllers();

app.Run();
