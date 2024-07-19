using HangFireApi.Service;
using MongoDB.Driver;

namespace HangFireApi.Infraestructure
{
    public static class ContextExtension
    {
        private const string DATABASE_CONNECTION_STRING = "mongodb://localhost:27017/";
        public static IServiceCollection AddMongoContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IMongoClient>((sp) =>
            {
                var database = DATABASE_CONNECTION_STRING;
                return new MongoClient(database);
            });
            services.AddSingleton<EmpladoRepository>();
            services.AddSingleton<ProgramacionRespository>();
            services.AddScoped<TurnoService>();
            services.AddScoped<ProgramacionService>();


            return services;
        }
    }
}
