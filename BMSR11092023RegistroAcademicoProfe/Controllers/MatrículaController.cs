//Importa el espacio de nombres necesarios para la autorizacion
using Microsoft.AspNetCore.Authorization;
//Importa el espacio de nombres necesarios para trabajar con controladores
using Microsoft.AspNetCore.Mvc;
//Importa el espacio de nombres necesarios para trabajar con modelos
using BMSR11092023RegistroAcademicoProfe.Models;

namespace BMSR11092023RegistroAcademicoProfe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Aplica la autorizacion a las acciones, lo que significa que solo los usuarios autenticados pueden acceder a ellas 
    [Authorize]
    public class MatrículaController : ControllerBase
    {
        //Declaracion de una lista estatica de objetos 'Matriculas' para almacanea matriculas registradas
        static List<Matriculas> matricula = new List<Matriculas>();

        [HttpPost("CrearMatricula")]
        public IActionResult Post([FromBody] Matriculas matriculas)
        {
            matricula.Add(matriculas);
            return Ok();
        }



        //Definicion de un metodo HTTP PUT para actualizar un cliente existente en la lista por su ID
        [HttpPut("{id}/ModificarMatricula")]

        public IActionResult Put(int id, [FromBody] Matriculas matriculas)
        {
            var existingMatricula = matricula.FirstOrDefault(M => M.Id == id);
            if (existingMatricula != null)
            {

                existingMatricula.NameStudent = matriculas.NameStudent;
                existingMatricula.Classroom = matriculas.Classroom;

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        //Definicion de un metodo HTTP GET que recibe un ID como parametro y devuelve una Matricula especifica
        [HttpGet("{id}/ObtenerPorIdMatricula")]
        public Matriculas Get(int id)
        {
            var matriculas = matricula.FirstOrDefault(M => M.Id == id);
            return matriculas;
        }
    }
}