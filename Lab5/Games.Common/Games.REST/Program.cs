using System.Text;
using Games.Infrastructure;
using Games.Infrastructure.Models;
using Games.Infrastructure.Repository;
using Games.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// --- DB path in project data folder
var dataDir = Path.Combine(builder.Environment.ContentRootPath, "data");
Directory.CreateDirectory(dataDir);
var dbPath = Path.Combine(dataDir, "games.db");
var connString = $"Data Source={dbPath}";

// Add DbContext
builder.Services.AddDbContext<GamesContext>(options =>
    options.UseSqlite(connString));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<GamesContext>()
    .AddDefaultTokenProviders();

// JWT setup (example key Ч move to secrets in real app)


var jwtKey = builder.Configuration["Jwt:Key"]!;

// Debug info Ч перев≥р€Їмо, €кий ключ реально читаЇтьс€
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
Console.WriteLine("=======================================");
Console.WriteLine($"Jwt:Key (raw) = {jwtKey}");
Console.WriteLine($"Jwt:Key length (chars) = {jwtKey.Length}");
Console.WriteLine($"Jwt:Key length (bytes) = {keyBytes.Length}");
Console.WriteLine($"Jwt:Key length (bits) = {keyBytes.Length * 8}");
Console.WriteLine("=======================================");

// Create symmetric key
var key = new SymmetricSecurityKey(keyBytes);

Console.WriteLine($"JWT key (raw): {jwtKey}");
Console.WriteLine($"JWT key byte length = {Encoding.UTF8.GetBytes(jwtKey).Length * 8} bits");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = key
    };

});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please insert JWT token into field",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<GamesContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
