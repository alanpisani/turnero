using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Turnero.Service;
using Turnero.Repositories.Interfaces;
using Turnero.Repositories;
using Turnero.Validators.UsuarioValidators;
using FluentValidation;
using Turnero.Validators.ProfesionalValidators;
using Turnero.Validators.TurnoValidators;
using Turnero.Validators.AuthValidators;
using Turnero.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
	builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);
}

builder.Configuration.AddEnvironmentVariables();


var connectionString = builder.Configuration.GetConnectionString("connection");

Console.WriteLine("VAR DE ENTORNO:");
Console.WriteLine(Environment.GetEnvironmentVariable("ConnectionStrings__connection"));

Console.WriteLine("=== CONNECTION STRING ACTUAL ===");
Console.WriteLine(connectionString);
// Add services to the container.

builder.Services.AddDbContext<TurneroContext>(options =>
	options.UseMySql(
		connectionString,
		MySqlServerVersion.Parse("8.0.33"),
		mySqlOptions =>
		{
			mySqlOptions.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(10),
				errorNumbersToAdd: null
			);

			mySqlOptions.CommandTimeout(30);
		}
	)
);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
			RoleClaimType = "rol",
		};
	});

builder.Services.AddAuthorization();

builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioRapidoValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<ProfesionalCreateValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<TurnoCreateValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidation>();


builder.Services.AddControllers(options =>
{
	options.Filters.Add<FluentValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<ProfesionalService>();
builder.Services.AddScoped<TurnoService>();
builder.Services.AddScoped<EspecialidadService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IHorarioLaboralRepository, HorarioLaboralRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
builder.Services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("PermitirFrontend",
		policy =>
		{
			policy.WithOrigins(
				"https://turnero-z7wo.onrender.com",
				"http://127.0.0.1:5500",
				"http://localhost:3000",
				"http://localhost:5173"
			)
			.AllowAnyHeader()
			.AllowAnyMethod();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("PermitirFrontend");

app.UseMiddleware<Turnero.Common.Middlewares.AuthTokenMiddleware>();
app.UseMiddleware<Turnero.Common.Middlewares.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
