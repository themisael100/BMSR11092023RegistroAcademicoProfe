using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BMSR11092023RegistroAcademicoProfe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            //Coprueba si las credenciales son validas
            if (login == "admin" && password == "12345")
            {
                //Crea una lista de reclamaciones(claims)
                var claims = new List<Claim>
                {
                    //Agrega una reclamacion de nombres con e valor de 'login'
                    new Claim(ClaimTypes.Name, login),
                };

                //Crea una identidad de reclamaciones (claims identity)
                // Con el esquema de autenticacion por cookies
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                //Crea propiedades de autenticacion adicionales
                //(puedes crear mas aqui si es necesario)
                var authProperties = new AuthenticationProperties();
                {
                    //Puedes configurar mas propiedades adicionales aqui
                }
                //Inicia sesion del usuario
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                //Devuelve una respuesta exitosa
                return Ok("Inicio de sesion correctamente");
            }
            else
            {
                //Devuelve una respuesta no autorizada si las credenciales son incorrectas
                return Unauthorized("Credenciales incorrectas");
            }
        }
        [HttpPost("logout")]
        public IActionResult logout()
        {
            //Cierra la sesion del usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Devuelve una respuesta exitosa
            return Ok("Cerro sesion correctamente.");
        }
    }
}