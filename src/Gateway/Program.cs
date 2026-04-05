    using Scalar.AspNetCore;

    var builder = WebApplication.CreateBuilder(args);

    // 1. Registro de YARP (Lee la sección "ReverseProxy" del appsettings.json)
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

    var app = builder.Build();

    app.UseHttpsRedirection();

    //Scalar
    if (app.Environment.IsDevelopment())
    {
        app.MapScalarApiReference(options =>
        {
            options.Title = "Gym Microservices API";

            // 1. ESTO ES LO MÁS IMPORTANTE:
            // Le decimos a Scalar: "Para cualquier documento, busca el JSON en /{documentName}/openapi/v1.json"
            options.OpenApiRoutePattern = "/{documentName}/openapi/v1.json";
            
            // 2. Registramos los documentos con nombres que COINCIDAN con tus prefijos de YARP
            // Tus rutas en appsettings son: /auth/, /entrenamiento/, /gestion/
            options.AddDocument("auth", "");
            options.AddDocument("entrenamiento", "");
            options.AddDocument("gestion", "");
        });
    }


    // 3. ¡IMPORTANTE! El orden importa: MapReverseProxy debe ir al final
    // para que no interfiera con otras rutas si las tuvieras.
    app.MapReverseProxy();

    app.Run();
