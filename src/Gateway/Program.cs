using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Registro de YARP (Lee la sección "ReverseProxy" del appsettings.json)
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// 2. Seguridad básica
app.UseHttpsRedirection();

//Scalar
app.MapScalarApiReference(options=>
    {
    options.Title = "Gym Microservices API";

    options.AddDocument("Autenticacion","https://localhost:5069/openapi/v1.json");
    options.AddDocument("Entrenamiento","https://localhost:5141/openapi/v1.json");
    options.AddDocument("Clientes","https://localhost:5071/openapi/v1.json");
});

// 3. ¡IMPORTANTE! El orden importa: MapReverseProxy debe ir al final
// para que no interfiera con otras rutas si las tuvieras.
app.MapReverseProxy();

app.Run();
