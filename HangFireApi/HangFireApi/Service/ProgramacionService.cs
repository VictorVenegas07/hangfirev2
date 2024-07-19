using Hangfire;
using HangFireApi.Model;

namespace HangFireApi.Service;

public class ProgramacionService
{
    private readonly ProgramacionRespository respository;

    public ProgramacionService(ProgramacionRespository respository)
    {
        this.respository = respository;
    }

    public void Configure(Programacion schedule)
    {
        CreateJobs(schedule, schedule.DaysAvailableRoute);
    }

    private void CreateJobs(Programacion schedule, List<DaysAvailableRoute> diasViables)
    {
        foreach (var dayRoute in diasViables)
        {
            if (!dayRoute.IsActive)
                continue;

            var cronExpression = GenerateCronExpression((int)dayRoute.Day, dayRoute.TimeStart.ToString());
            var timeZone = TimeZoneInfo.Local;
            dayRoute.JobId = $"{schedule.Name}_{dayRoute.Day}_{dayRoute.TimeStart}";
            RecurringJob.AddOrUpdate(dayRoute.JobId, () => ExecuteTask(schedule.Name), cronExpression, timeZone);

        }
    }

    public void UpdateJobs(Programacion programacion)
    {
        var diasConJobId = programacion.DaysAvailableRoute.Where(x => x.JobId is not null && x.IsActive).ToList();
        diasConJobId.ForEach(dayRoute =>
            {
                var cronExpression = GenerateCronExpression((int)dayRoute.Day, dayRoute.TimeStart.ToString());
                var timeZone = TimeZoneInfo.Local;
                RecurringJob.AddOrUpdate(dayRoute.JobId, () => ExecuteTask(programacion.Name), cronExpression, timeZone);
            }
        );

        var diasSinJobId = programacion.DaysAvailableRoute.Where(x => x.JobId is null && x.IsActive).ToList();
        if (diasSinJobId.Any())
            CreateJobs(programacion, diasSinJobId);
    }

    private string GenerateCronExpression(int dayOfWeek, string time)
    {
        var timeParts = time.Split(':');
        return Cron.Weekly((DayOfWeek)dayOfWeek, int.Parse(timeParts[0]), int.Parse(timeParts[1]));
    }

    public void ExecuteTask(string taskName)
    {
        // Tu lógica para ejecutar la tarea
        Console.WriteLine($"Ejecutando tarea: {taskName}");
    }

}
