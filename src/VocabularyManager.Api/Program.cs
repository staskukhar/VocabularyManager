using VocabularyManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VocabularyManager.Api.ExceptionHandlers;
using VocabularyManager.Api.DIExtensions;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddValidationFilters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string keycloakAuthority = builder.Configuration["Keycloak:Authority"]
    ?? throw new InvalidOperationException("Keycloak:Authority configuration is required.");
string? keycloakValidIssuer = builder.Configuration["Keycloak:ValidIssuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = keycloakAuthority;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = keycloakValidIssuer ?? keycloakAuthority,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });
builder.Services.AddAuthorization();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<VocabularyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VocabularyDbConnection")));
builder.Services.InjectDependencies();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    VocabularyContext context = scope.ServiceProvider.GetRequiredService<VocabularyContext>();
    bool schemaExists = context.Database
        .SqlQueryRaw<int>(
            @"SELECT COUNT(*)::int AS ""Value"" FROM information_schema.tables
              WHERE table_schema = 'public' AND table_name = 'Vocabularies'")
        .AsEnumerable()
        .First() > 0;

    if (!schemaExists)
    {
        await context.Database.MigrateAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

await app.RunAsync();