using VocabularyManager.Infrastructure.Data;
using VocabularyManager.Core.Services;
using VocabularyManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.UseCases.Validators;
using FluentValidation;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;
using VocabularyManager.Api.Middleware;
using VocabularyManager.Api.ActionFilters;
using VocabularyManager.UseCases.Services;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<WordValidationFilter>();
builder.Services.AddScoped<WordsValidationFilter>();
builder.Services.AddScoped<VocabularyValidationFilter>();
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
builder.Services.AddScoped<IRepositoryBase<Vocabulary>, EfRepository<Vocabulary>>();
builder.Services.AddScoped<IWordStoreManager, WordStoreManager>();
builder.Services.AddScoped<IVocabularyStoreManager, VocabularyStoreManager>();
builder.Services.AddScoped<IWordParser<WordDTO>, OxfordDictionaryParser>();
builder.Services.AddScoped<IValidator<Word>, WordValidator>();
builder.Services.AddScoped<IValidator<Vocabulary>, VocabularyValidator>();
builder.Services.AddScoped<IValidator<WordDTO>, OxfordParsingWordDTOValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
