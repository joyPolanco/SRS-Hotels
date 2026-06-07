using Carter;
using SRS_Hotels.Data.Exceptions;
using SRS_Hotels.Data.src.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    
    }
);
var app = builder.Build();

app.UseExceptionHandler(opt => { });

app.MapCarter();


app.Run();
