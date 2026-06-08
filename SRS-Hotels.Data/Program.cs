using Carter;
using FluentValidation;
using Mapster;
using SRS_Hotels.Data.Exceptions;
using SRS_Hotels.Data.src.BuildingBlocks.Behaviors;
using SRS_Hotels.Data.src.Infraestructure;
using SRS_Hotels.Data.src.Infraestructure.Identity.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddMapster();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    
    }
);

var app = builder.Build();
await IdentitySeederRunner.RunAsync(app);

app.UseExceptionHandler(opt => { });

app.MapCarter();


app.Run();
