var builder = WebApplication.CreateBuilder(args);

// 1. Registro de YARP (Lee la sección "ReverseProxy" del appsettings.json)
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// 2. Seguridad básica
app.UseHttpsRedirection();

// 3. ¡IMPORTANTE! El orden importa: MapReverseProxy debe ir al final
// para que no interfiera con otras rutas si las tuvieras.
app.MapReverseProxy();

app.Run();
