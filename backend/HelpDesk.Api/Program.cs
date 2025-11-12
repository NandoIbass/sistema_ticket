using HelpDesk.Application.Interfaces;
using HelpDesk.Infrastructure.Persistence;
using HelpDesk.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Serilog (console)
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration).WriteTo.Console());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext - SQLite
builder.Services.AddDbContext<HelpDeskDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI Repos
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

// FluentValidation (si lo instalas): builder.Services.AddValidatorsFromAssemblyContaining<CreateTicketValidator>();

// CORS
builder.Services.AddCors(options => options.AddPolicy("AllowFrontend", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
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
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Global simple error handling
app.Use(async (context, next) =>
{
    try { await next(); }
    catch (Exception ex)
    {
        Log.Error(ex, "Unhandled exception");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Error interno" });
    }
});

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Run migrations automatically (créalo en dev)
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<HelpDeskDbContext>();
    ctx.Database.Migrate();
}

app.Run();
