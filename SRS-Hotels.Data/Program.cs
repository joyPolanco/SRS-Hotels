using Carter;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SRS_Hotels.Data.Exceptions;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Behaviors;
using SRS_Hotels.Data.src.BuildingBlocks.Helpers;
using SRS_Hotels.Data.src.Infraestructure;
using SRS_Hotels.Data.src.Infraestructure.Identity.Seeds;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddMapster();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddSingleton<IRetryPolicy, RetryPolicyService>(); 

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = "Bearer";
    opt.DefaultChallengeScheme = "Bearer";
})
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]!))



        };
    });
builder.Services.AddAuthorization();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    
    }
);

var app = builder.Build();
await IdentitySeederRunner.RunAsync(app);

app.UseExceptionHandler(opt => { });
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();


app.Run();
