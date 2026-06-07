using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions.SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Configurations;
using SRS_Hotels.Data.src.Infraestructure.Identity.Contexts;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;
using SRS_Hotels.Data.src.Infraestructure.Persistence.Contexts;
using SRS_Hotels.Data.src.Infraestructure.Persistence.Repositories;
using SRS_Hotels.Data.src.Infraestructure.Shared.Services;
using SRS_Hotels.Data.src.Infraestructure.Shared.Services.SRS_Hotels.Infrastructure.Authentication;

namespace SRS_Hotels.Data.src.Infraestructure
{
    public  static class DependencyInjection
    {

        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration config)
        {
            //Services
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IIdentityAccountService, IdentityAccountService>();
            services.AddScoped<IIdentityPasswordService, IdentityPasswordService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //Options
            services.Configure<JwtOptions>(opt =>
            {
                config.GetSection("JwtOptions").Bind(opt);
            });


          

            services.AddDbContext<HotelContext>(options =>
                options.UseInMemoryDatabase("DbHotel"));




            //IDENTITY

            services.AddDbContext<IdentityContext>(options =>
                options.UseInMemoryDatabase("DbIdentity"));

            //Configuraciones
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireNonAlphanumeric = true;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequiredUniqueChars = 1;

                    opt.User.RequireUniqueEmail = true;
                    opt.SignIn.RequireConfirmedEmail = true;



                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                    opt.Lockout.MaxFailedAccessAttempts = 5;


                    opt.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
                    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;


                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();


            //Cookies
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });





            return services;
        }
    }
}
