
//Importa el espacio de nombres necesaios para la autentificacion por cookies
using Microsoft.AspNetCore.Authentication.Cookies;
//Importa el espacio de nombres necesarios para trabajar con JSON
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//agrega servicios al contenedor de dependencias.
//Agrega el servicio de controladores al servidor.
builder.Services.AddControllers();
//Agerga el sevicio para la exploracion de la API de putos finales
builder.Services.AddEndpointsApiExplorer();
//Agrega el servicio para la generacion de Swagger
builder.Services.AddSwaggerGen();


//Configuracion para la autentificacion por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //Configura el nombre del parametro de URL para redireccionamiento de no autorizado
        options.ReturnUrlParameter = "unauthorized";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                //Cambia el codigo de estado a No autorizado
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //Establece el tipo de contenido como JSON (u otro formato deseado)
                context.Response.ContentType = "application/json";
                var message = new
                {
                    error = "No Autorizado",
                    message = "Debe iniciar sesion para acceder a este recurso."
                };
                //Serializa el objeto "message" en formato
                //JSON (puede usar otro serializador JSON si lo desea)
                var jsonMessaje = JsonSerializer.Serialize(message);
                //Escribe elmensaje JSON en la respuesta HTTP
                return context.Response.WriteAsync(jsonMessaje);
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();