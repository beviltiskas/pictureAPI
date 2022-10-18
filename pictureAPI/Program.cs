using Microsoft.AspNetCore.Hosting;
using pictureAPI.Data;
using pictureAPI.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PictureAPIDbContext>();
builder.Services.AddTransient<IPicturesRepository, PicturesRepository>();
builder.Services.AddTransient<IAlbumsRepository, AlbumsRepository>();
builder.Services.AddTransient<IPortfoliosRepository, PortfoliosRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
