using ApiCentroMedico.Dto.Especialidades;
using ApiCentroMedico.Dto.Medicos;
using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Dto.Pacientes;
using ApiCentroMedico.Dto.Turnos;
using ApiCentroMedico.Dto.Usuario;
using ApiCentroMedico.MappingProfile;
using ApiCentroMedico.Models;
using ApiCentroMedico.Repository;
using ApiCentroMedico.Services;
using ApiCentroMedico.UnitWork;
using ApiCentroMedico.Validators.Especialidades;
using ApiCentroMedico.Validators.Medicos;
using ApiCentroMedico.Validators.ObrasSociales;
using ApiCentroMedico.Validators.Pacientes;
using ApiCentroMedico.Validators.Turnos;
using ApiCentroMedico.Validators.Usuarios;
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

            builder.Services.AddScoped<ICommonService<MedicoDto, MedicoInsertDto, MedicoUpdateDto>, MedicoService>();
            builder.Services.AddScoped<IMedicoService, MedicoService>();

            builder.Services.AddKeyedScoped<ICommonService<EspecialidadDto, EspecialidadInsertDto, EspecialidadDto>, EspecialidadService>("EspecialidadService");
            builder.Services.AddKeyedScoped<ICommonService<ObraSocialDto, ObraSocialInsertDto, ObraSocialUpdateDto>, ObraSocialService>("ObraSocialService");

            builder.Services.AddScoped<ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto>, PacienteService>();
            builder.Services.AddScoped<IPacienteService, PacienteService>();

            builder.Services.AddKeyedScoped<ICommonService<TurnoDto,TurnoInsertDto,TurnoUpdateDto>,TurnoService >("TurnoService");
            builder.Services.AddKeyedScoped<IAuthenticationService, AuthenticationService>("AuthenticationService");
            #endregion

            #region Repositories for Services

            builder.Services.AddScoped<IRepository<Especialidade>, Repository<Especialidade>>();
            builder.Services.AddScoped<IRepository<ObrasSociale>, Repository<ObrasSociale>>();
            builder.Services.AddScoped<IRepository<Turno>, Repository<Turno>>();

            builder.Services.AddScoped<IRepository<Medico>, MedicoRepository>();
            builder.Services.AddScoped<IMedicoRepository,MedicoRepository>();
            
            builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
            builder.Services.AddScoped<IRepository<Paciente>,PacienteRepository>();

            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            #endregion

            #region Validators

            #region Medicos
            builder.Services.AddScoped<IValidator<MedicoWithUserDto>, MedicoWithUserValidator>();
            builder.Services.AddScoped<IValidator<MedicoUpdateDto>, MedicoUpdateValidator>();
            #endregion

            #region Pacientes
            builder.Services.AddScoped<IValidator<PacienteWithUserDto>, PacienteWithUserValidator>();
            builder.Services.AddScoped<IValidator<PacienteUpdateDto>, PacienteUpdateValidator>();
            #endregion

            #region Turnos
            builder.Services.AddScoped<IValidator<TurnoInsertDto>, TurnoInsertValidator>();
            #endregion

            #region Usuarios
            builder.Services.AddScoped<IValidator<UserDto>, UsuarioValidator>();
            #endregion

            #region Especialidades
            builder.Services.AddScoped<IValidator<EspecialidadDto>, EspecialidadValidator>();
            builder.Services.AddScoped<IValidator<EspecialidadInsertDto>, EspecialidadInsertValidator>();
            #endregion

            #region Obras Sociales
            builder.Services.AddScoped<IValidator<ObraSocialUpdateDto>, ObraSocialUpdateValidator>();
            builder.Services.AddScoped<IValidator<ObraSocialInsertDto>, ObraSocialInsertValidator>();
            #endregion

            #endregion

            #region UnitWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  
            
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddAutoMapper(typeof(Mapping));

            builder.Services.AddDbContext<CentromedicoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CentroMedicoContext"));
            });



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region JWTTOKENS
            //obtener mediante app config
            builder.Services.AddAuthorization(); //añado el servicio de autorizacion

            builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
                };

            }
                );
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Medico", policy => policy.RequireClaim("Type", "MEDICO"));
                opt.AddPolicy("Paciente", policy => policy.RequireClaim("Type", "PACIENTE"));
                opt.AddPolicy("Admin", policy => policy.RequireClaim("Type", "ADMINISTRADOR"));
                opt.AddPolicy("MedicoOrAdmin", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.Claims.Any(c =>
                            c.Type == "Type" && (c.Value == "MEDICO" || c.Value == "ADMINISTRADOR")
                        )
                    );
                });
                opt.AddPolicy("All", policy =>
                {
                    policy.RequireAssertion(context =>
                    context.User.Claims.Any(c =>
                                           c.Type == "Type" && (c.Value == "MEDICO" || c.Value == "ADMINISTRADOR" || c.Value == "PACIENTE")
                                           )
                                       );

                });

            });
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); //indico al middleware que maneje autenticacion, antes de la autorzacion
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
