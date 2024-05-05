using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICatProductosRepository, CatProductosRepository>();
builder.Services.AddScoped<ICatProductosService, CatProductosService>();

builder.Services.AddScoped<ICatTipoClienteRepository, CatTipoClienteRepository>();
builder.Services.AddScoped<ICatTipoClienteService, CatTipoClienteService>();

builder.Services.AddScoped<ITblClientesRepository, TblClientesRepository>();
builder.Services.AddScoped<ITblClientesService, TblClientesService>();

builder.Services.AddScoped<ITblDetallesFacturaRepository, TblDetallesFacturaRepository>();
builder.Services.AddScoped<ITblDetallesFacturaService, TblDetallesFacturaService>();

builder.Services.AddScoped<ITblFacturasRepository, TblFacturasRepository>();
builder.Services.AddScoped<ITblFacturasService, TblFacturasService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

//JWT
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; 
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowMyOrigin");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
