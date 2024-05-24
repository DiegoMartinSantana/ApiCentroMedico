using ApiCentroMedico.Dto.Medicos;
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

            // Add services to the container.
            builder.Services.AddKeyedScoped<ICommonServices<MedicoDto, MedicoInsertDto, MedicoUpdateDto>, MedicosService>("IMedicoServices");
            builder.Services.AddScoped<IRepository<Medico>, MedicoRepository>();

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
