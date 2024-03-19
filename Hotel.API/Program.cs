using Hotel.LogicaAccesoDatos.EntityFramework;
using Hotel.LogicaAccesoDatos.EntityFramework.RepositoriosEF;
using Hotel.LogicaAplicacion.CasosDeUso.TipoCabanha;
using Hotel.LogicaAplicacion.CasosDeUso.Cabanha;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Cabanha;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TipoCabanha;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Mantenimiento;
using Hotel.LogicaAplicacion.CasosDeUso.Mantenimiento;
using Hotel.LogicaAplicacion.CasosDeUso.TopesDescripcion;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TopesDescripcion;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Usuario;
using Hotel.LogicaAplicacion.CasosDeUso.Usuario;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Hotel.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hotel.API.xml")));

            var claveDificil = "Obligatorio2023_PrograMacionM3B_ClaveSuperSecreta15";
            var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
            var issuer = builder.Configuration.GetValue<string>("ValidIssuer");
            var audience = builder.Configuration.GetValue<string>("ValidAudience");

            #region Conexión BD
            builder.Services.AddDbContext<HotelContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("ConexionHotel_Api")
                    )
                );
            #endregion

            #region Agregar mis servicios para inyecciones de dependencias

            //Registrar los repositorios
            builder.Services.AddScoped<IRepositorioTipoCabanha, RepositorioTipoCabanha>();
            builder.Services.AddScoped<IRepositorioCabanha, RepositorioCabanha>();
            builder.Services.AddScoped<IRepositorioMantenimiento, RepositorioMantenimiento>();
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<IRepositorioTopesDescripcion, RepositorioTopesDescripcion>();

            //Registrar los casos de uso
            #region Tipo Cabanha
            builder.Services.AddScoped<IAddTipoCabanha, AddTipoCabanha>();
            builder.Services.AddScoped<IDeleteTipoCabanha, DeleteTipoCabanha>();
            builder.Services.AddScoped<IGetAllTipoCabanha, GetAllTipoCabanha>();
            builder.Services.AddScoped<IGetByIdTipoCabanha, GetByIdTipoCabanha>();
            builder.Services.AddScoped<IUpdateTipoCabanha, UpdateTipoCabanha>();
            builder.Services.AddScoped<IBuscarPorNombreTipoCabanha, BuscarPorNombreTipoCabanha>();
            #endregion

            #region Cabanha
            builder.Services.AddScoped<IAddCabanha, AddCabanha>();
            builder.Services.AddScoped<IDeleteCabanha, DeleteCabanha>();
            builder.Services.AddScoped<IGetAllCabanhas, GetAllCabanhas>();
            builder.Services.AddScoped<IGetByIdCabanha, GetByIdCabanha>();
            builder.Services.AddScoped<IUpdateCabanha, UpdateCabanha>();
            builder.Services.AddScoped<IBuscarTextoEnNombreCabanha, BuscarTextoEnNombreCabanha>();
            builder.Services.AddScoped<IBuscarPorTipoCabanha, BuscarPorTipoCabanha>();
            builder.Services.AddScoped<IBuscarPorMaxPersonas, BuscarPorMaxPersonas>();
            builder.Services.AddScoped<IBuscarPorHabilitada, BuscarPorHabilitada>();
            builder.Services.AddScoped<IConsulta6A, Consulta6A>();
            #endregion

            #region Mantenimiento
            builder.Services.AddScoped<IAddMantenimiento, AddMantenimiento>();
            builder.Services.AddScoped<IGetAllMantenimientos, GetAllMantenimientos>();
            builder.Services.AddScoped<IFindAllMantenimientosCabanhas, FindAllMantenimientosCabanhas>();
            builder.Services.AddScoped<IGetByIdMantenimiento, GetByIdMantenimiento>();
            builder.Services.AddScoped<IMantenimientoEntreFechas, MantenimientoEntreFechas>();
            builder.Services.AddScoped<IUpdateMantenimiento, UpdateMantenimiento>();
            builder.Services.AddScoped<IDeleteMantenimiento, DeleteMantenimiento>();
            #endregion

            #region Topes Descripcion
            builder.Services.AddScoped<IAddTopesDescripcion, AddTopesDescripcion>();
            builder.Services.AddScoped<IGetAllTopesDescripcion, GetAllTopesDescripcion>();
            builder.Services.AddScoped<IUpdateTopesDescripcion, UpdateTopesDescripcion>();
            builder.Services.AddScoped<IFindByNameObjectTopesDescripcion, FindByNameObjectTopesDescripcion>();
            #endregion

            #region Usuario
            builder.Services.AddScoped<IAddUsuario, AddUsuario>();
            builder.Services.AddScoped<IValidarLoginUsuario, ValidarLoginUsuario>();
            #endregion
            #endregion

            #region Registro de servicios JWT
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        //Definir las verificaciones a realizar
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        //Definir los valores a intercambiar
                        ValidIssuer = "identificadorEmisor",
                        ValidAudience = "identificadorAudiencia",
                        IssuerSigningKey = claveDificilEncriptada
                    };

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}