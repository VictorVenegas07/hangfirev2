using HangFireApi.Model;
using MongoDB.Driver;

namespace HangFireApi.Service
{
    public class TurnoService
    {
        private readonly EmpladoRepository _mongoDBService;

        public TurnoService(EmpladoRepository mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        public async Task VerificarTurnosAsync()
        {
            var empleados = await _mongoDBService.GetEmpleadosAsync();
            var now = DateTime.UtcNow;

            foreach (var empleado in empleados)
            {
                bool estaActivo = now >= empleado.HoraDeInicio && now <= empleado.HoraFinal;
                if (empleado.EstaActivo != estaActivo)
                {
                    empleado.EstaActivo = estaActivo;
                    await _mongoDBService.UpdateEmpleadoAsync(empleado.Id, empleado);
                }
            }
        }

        public List<Empleado> BuscarPersonaPorFecha(DateTime fechaNacimiento)
            => _mongoDBService.BuscarPersonaPorFecha(fechaNacimiento);

        public List<Empleado> BuscarEmpleadosVigentes() 
            => _mongoDBService.BuscarEmpleadosVigentes(DateTime.UtcNow);
        

    }
}
