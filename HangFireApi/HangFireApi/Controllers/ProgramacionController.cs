using Hangfire;
using HangFireApi.Model;
using HangFireApi.Request;
using HangFireApi.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HangFireApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramacionController : ControllerBase
    {

        private readonly ProgramacionRespository _mongoDBService;
        private readonly ProgramacionService _programacionService;

        public ProgramacionController(ProgramacionRespository mongoDBService, ProgramacionService programacionService)
        {
            _mongoDBService = mongoDBService;
            _programacionService = programacionService;
        }

        [HttpPost]
        public async Task<ActionResult> CrearEmpleado([FromBody] ProgramacionRequest empleado)
        {
            try
            {
                var programacion = MapearProgramacion(empleado);
                await _mongoDBService.InsertOneAsync(programacion);
                _programacionService.Configure(programacion);
                await _mongoDBService.UpdateEmpleadoAsync(programacion.Id, programacion);

                return Ok(new { mensaje = "Programacion creado correctamente", empleado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la Programacion", error = ex.Message });
            }
        }
        private Programacion MapearProgramacion(ProgramacionRequest programacion)
        {
            var diasViables = programacion.DaysAvailableRoute
                .Select(d => new DaysAvailableRoute(d.Day, d.TimeStart, d.TimeEnd, d.IsActive)).ToList();
            return new Programacion(programacion.Name, programacion.DateStart, programacion.DateEnd, diasViables);
        }

        [HttpDelete("{jobId}")]
        public IActionResult DeleteJob(string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);
            return Ok($"Job with ID {jobId} has been deleted.");
        }


        [HttpPut]
        public async Task<ActionResult> CrearEmpleado([FromBody] ProgramacionRequest empleado, [FromQuery] string id)
        {
            try
            {
                var programacion = MapearProgramacion(empleado);
                programacion.Id = id;
                await _mongoDBService.UpdateEmpleadoAsync(id, programacion);
                _programacionService.UpdateJobs(programacion);
                await _mongoDBService.UpdateEmpleadoAsync(programacion.Id, programacion);

                return Ok(new { mensaje = "Programacion creado correctamente", empleado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la Programacion", error = ex.Message });
            }
        }
    }
}
