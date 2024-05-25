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

namespace ApiCentroMedico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ServicesForControllers

            builder.Services.AddKeyedScoped<ICommonService<MedicoDto, MedicoInsertDto, MedicoUpdateDto>, MedicoService>("MedicoService");
            builder.Services.AddKeyedScoped<ICommonService<EspecialidadDto, EspecialidadDto, EspecialidadDto>, EspecialidadService>("EspecialidadService");
            builder.Services.AddKeyedScoped<ICommonService<Obra_SocialDto, Obra_SocialDto, ObraSocialUpdateDto>, ObraSocialService>("ObraSocialService");
            builder.Services.AddKeyedScoped<ICommonService<PacienteDto, PacienteInsertDto, PacienteUpdateDto>, PacienteService>("PacienteService");
            builder.Services.AddKeyedScoped<ITurnoService, TurnoService>("TurnoService");

            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<Medico>, MedicoRepository>();
            builder.Services.AddScoped<IRepository<Especialidade>, EspecialidadRepository>();
            builder.Services.AddScoped<IRepository<ObrasSociale>, ObraSocialRepository>();
            builder.Services.AddScoped<IRepository<Paciente>, PacienteRepository>();
            builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();

            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddAutoMapper(typeof(Mapping));

            builder.Services.AddDbContext<DiagnosticoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DiagnosticoContext"));
            });
            #region Validators
            builder.Services.AddScoped<IValidator<MedicoInsertDto>, MedicoInsertValidator>();
            #endregion
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
