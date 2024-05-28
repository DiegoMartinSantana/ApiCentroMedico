using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.MappingProfile;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using ApiCentroMedico.Services;
using ApiCentroMedico.Validators.Medicos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiCentroMedico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            #region ServicesForControllers

            builder.Services.AddKeyedScoped<MedicoService>("MedicoService");
            builder.Services.AddKeyedScoped<ICommonService<EspecialidadDto, EspecialidadDto, EspecialidadDto>, EspecialidadService>("EspecialidadService");
            builder.Services.AddKeyedScoped<ICommonService<Obra_SocialDto, Obra_SocialDto, ObraSocialUpdateDto>, ObraSocialService>("ObraSocialService");
            builder.Services.AddKeyedScoped<ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto>, PacienteService>("PacienteService");
            builder.Services.AddKeyedScoped<ITurnoService, TurnoService>("TurnoService");
            builder.Services.AddKeyedScoped<IAuthenticationService,AuthenticationService>("AuthenticationService");
            #endregion

            #region Repositories for Services
            builder.Services.AddScoped<MedicoRepository, MedicoRepository>(); // inyecto la clase, porque tiene cosas propias.
            builder.Services.AddScoped<IRepository<Especialidade>, EspecialidadRepository>();
            builder.Services.AddScoped<IRepository<ObrasSociale>, ObraSocialRepository>();
            builder.Services.AddScoped<IRepository<Paciente>, PacienteRepository>();
            builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
            builder.Services.AddScoped<IAuthenticationRepository,AuthenticationRepository>();

            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddAutoMapper(typeof(Mapping));

            builder.Services.AddDbContext<CentromedicoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CentroMedicoContext"));
            });
            #region Validators
            builder.Services.AddScoped<IValidator<MedicoInsertDto>, MedicoInsertValidator>();
            #endregion
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region JWTTOKENS
            builder.Services.AddAuthorization();
            //obtener mediante app config
            builder.Services.AddAuthentication("Bearer").AddJwtBearer( opt =>
            {
                var SignignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var SigningCredentials = new SigningCredentials(SignignKey, SecurityAlgorithms.HmacSha256Signature);
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false, ValidateIssuer=false,
                    IssuerSigningKey = SignignKey
                };

            }
                
                );
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            


            app.MapControllers();

            app.Run();
        }
    }
}
