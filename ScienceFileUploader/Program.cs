using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScienceFileUploader.BackgroundWorker;
using ScienceFileUploader.Data;
using ScienceFileUploader.Repository;
using ScienceFileUploader.Repository.Interface;
using ScienceFileUploader.Service;
using ScienceFileUploader.Service.Interface;
using ValueRepository = ScienceFileUploader.Repository.ValueRepository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<IValueRepository, ValueRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IValueService, ValueService>();
builder.Services.AddSingleton<FileStorageQueue>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<DataContext>())
    context.Database.Migrate();

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