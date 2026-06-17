using System.Text;
using LeadsProject.Data;
using LeadsProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Convert Render's postgres:// URL to Npgsql format
if (connStr.StartsWith("postgres://") || connStr.StartsWith("postgresql://"))
{
    var uri = new Uri(connStr);
    var userInfo = uri.UserInfo.Split(':');
    var port = uri.Port > 0 ? uri.Port : 5432;
    connStr = $"Host={uri.Host};Port={port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
}

if (connStr.Contains("Host=") || connStr.Contains("host="))
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connStr));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connStr));
}

builder.Services.AddScoped<JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAngular", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.IsNpgsql())
    {
        db.Database.EnsureCreated();
        // Seed users if empty
        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new LeadsProject.Models.User { Name = "מנהל", PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"), Role = "manager" },
                new LeadsProject.Models.User { Name = "אריה", Role = "agent" },
                new LeadsProject.Models.User { Name = "דב", Role = "agent" },
                new LeadsProject.Models.User { Name = "רבקה", Role = "agent" },
                new LeadsProject.Models.User { Name = "מוישי", Role = "agent" },
                new LeadsProject.Models.User { Name = "מיכאל", Role = "agent" },
                new LeadsProject.Models.User { Name = "אהרון", Role = "agent" },
                new LeadsProject.Models.User { Name = "ליסה", Role = "agent" }
            );
            db.SaveChanges();
        }
    }
    else
    {
        db.Database.Migrate();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowAngular");

// Handle errors with CORS headers
app.Use(async (context, next) =>
{
    try { await next(); }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync($"{{\"error\": \"{ex.Message.Replace("\"", "'")}\"}}");
    }
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
