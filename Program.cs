using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TheComputerShop.ComputerShopMapper;
using TheComputerShop.DATA;
using TheComputerShop.Models;
using TheComputerShop.Repository;
using TheComputerShop.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);


//Configuramos la conexion a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

//Soporte para autenticación con .NET Identity
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//Añadimos Cache
builder.Services.AddResponseCaching();

//Agregamos los repositorios
#region Repositorios

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddScoped<IManufacturersRepository, ManufacturersRepository>();

#endregion

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAutoMapper(typeof(ComputerShopMapper));

//Aquí se configura la Autenticación
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; //Para que no se requiera HTTPS para la autenticación de la aplica
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer. \r\n\r\n" +
        "Ingrese la palabra 'Bearer' seguida de un [espacio] y despues su Token \r\n\r\n" +
        "Ejemplo: \"Bearer tgdfdgrd\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Soporte para Cors
app.UseCors("PolicyCors");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
