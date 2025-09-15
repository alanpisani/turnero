using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Turnero.Common.Helpers;
using Turnero.Models;
using Turnero.Service;
using Turnero.Repositories.Interfaces;
using Turnero.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
	   .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
	   .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("connection");

// Add services to the container.

builder.Services.AddDbContext<TurneroContext>(options =>
	options.UseMySql(
		connectionString,                               // la variable
		new MySqlServerVersion(new Version(8, 0, 33)) // versión de MySQL
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
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
		};
	});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(options =>
{
	options.Filters.Add<ValidateModelStateAttribute>();
})
.ConfigureApiBehaviorOptions(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<ProfesionalService>();
builder.Services.AddScoped<TurnoService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
builder.Services.AddScoped<IHorarioLaboralRepository, HorarioLaboralRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
builder.Services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<Turnero.Common.Middlewares.AuthTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
