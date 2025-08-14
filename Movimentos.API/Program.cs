using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movimentos.Business.Service;
using Movimentos.Business.Service.Interface;
using Movimentos.Business.Validators;
using Movimentos.CrossCutting.Auth;
using Movimentos.CrossCutting.Auth.Interface;
using Movimentos.CrossCutting.Logging;
using Movimentos.Data.Context;
using Movimentos.Data.Repositories;
using Movimentos.Data.Repositories.Interface;
using System.Text;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Movimentos.Entities.Entities;

namespace Movimentos.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string CorsPolicy = "_corsPolicy ";
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DBConnection");
            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IGenericRepository<Produto>, GenericRepository<Produto>>();
            builder.Services.AddScoped<IGenericRepository<ProdutoCosif>, GenericRepository<ProdutoCosif>>();
            builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            builder.Services.AddScoped<IMovimentoService, MovimentoService>();
            builder.Services.AddScoped<IProdutoCosifService, ProdutoCosifService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();

            builder.Services.AddSingleton<ILoggerService, ConsoleLoggerService>();
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            

            Log.Logger = new LoggerConfiguration()
                          .MinimumLevel.Debug()
                          .WriteTo.Console()
                          .WriteTo.File("logs/movimentos-.log", rollingInterval: RollingInterval.Day)
                          .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<MovimentoValidator>();

          


            builder.Services.AddCors(dbContextOptions =>
            {
                dbContextOptions.AddPolicy(CorsPolicy,
                   builder =>
                   {
                       builder.WithOrigins("http://localhost:4200/", "http://localhost:4200/")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                   });
            });

            builder.Services.AddEndpointsApiExplorer();

            // Configuração JWT
            var authSettings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();

            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
            builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = authSettings!.Issuer,
                            ValidAudience = authSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret))
                        };
                    });


            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
            app.Run();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Run();
        }
    }
}
