using HangFireApi.Model;
using HangFireApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace HangFireApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly EmpladoRepository _mongoDBService;

        public WeatherForecastController(EmpladoRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        } 

        [HttpPost]
        public async Task<ActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {
            try
            {
                // Insertar el empleado en MongoDB
                await _mongoDBService.InsertOneAsync(empleado);

                return Ok(new { mensaje = "Empleado creado correctamente", empleado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear el empleado", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Empleado>> GetEmploye()
        {
            return await _mongoDBService.GetEmpleadosAsync();
        }

        [HttpGet("obtenerFecha/{fechaNacimiento}")]
        public List<Empleado> GetEmployeByDate(DateTime fechaNacimiento)
        {
            return _mongoDBService.BuscarPersonaPorFecha(fechaNacimiento);
        }

        [HttpGet("obtenerEmpleadosVigentes")]
        public List<Empleado> GetEmployeByDate() =>_mongoDBService.BuscarEmpleadosVigentes(DateTime.UtcNow);
    }
}
