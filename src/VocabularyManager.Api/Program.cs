using VocabularyManager.Infrastructure.Data;
using VocabularyManager.Core.Services;
using VocabularyManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification;
using VocabularyManager.Core.Interfaces;
using DictionaryParser.Services;
using VocabularyManager.UseCases.Validators;
using FluentValidation;
using VocabularyManager.UseCases.DTOs;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7211", "http://localhost:5218")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<VocabularyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VocabularyDbConnection")));
builder.Services.AddScoped<IRepositoryBase<Word>, EfRepository<Word>>();
builder.Services.AddScoped<IRepositoryBase<WordList>, EfRepository<WordList>>();
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IWordListService, WordListService>();
builder.Services.AddScoped<IWordParser<Word>, OxfordDictionaryParser>();
builder.Services.AddScoped<AbstractValidator<WordDTO>, WordDTOValidator>();
builder.Services.AddScoped<AbstractValidator<WordListDTO>, WordListDTOValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
