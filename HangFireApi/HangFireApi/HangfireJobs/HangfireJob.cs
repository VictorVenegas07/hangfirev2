using Hangfire;
using HangFireApi.Service;

namespace HangFireApi.HangfireJobs
{
    public static class HangfireJob
    {
        public static void RegisterJobs()
        {
            // Registrar la tarea recurrente para verificar turnos cada minuto
            //RecurringJob.AddOrUpdate<TurnoService>(service => service.VerificarTurnosAsync(), Cron.Minutely);
        }
    }
}
