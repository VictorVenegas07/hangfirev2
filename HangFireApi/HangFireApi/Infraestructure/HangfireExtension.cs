using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Dashboard.BasicAuthorization;

namespace HangFireApi.Infraestructure
{
    public static class HangfireExtension
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration config)
        {
            var mongoConnectionString = config.GetConnectionString("MongoConnection");
            var databaseName = config["HangfireSettings:DatabaseName"];
            services.AddHangfire(configuration => configuration
                           .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                           .UseSimpleAssemblyNameTypeSerializer()
                           .UseRecommendedSerializerSettings()
                           .UseMongoStorage(mongoConnectionString, databaseName, new MongoStorageOptions
                           {
                               MigrationOptions = new MongoMigrationOptions
                               {
                                   MigrationStrategy = new MigrateMongoMigrationStrategy(),
                                   BackupStrategy = new CollectionMongoBackupStrategy()
                               },
                               Prefix = "hangfire",
                               CheckConnection = true,
                               QueuePollInterval = TimeSpan.FromSeconds(15),
                               JobExpirationCheckInterval = TimeSpan.FromHours(1),
                               CountersAggregateInterval = TimeSpan.FromMinutes(5),
                               DistributedLockLifetime = TimeSpan.FromMinutes(30)
                           }));

            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Inteia server 1";
            });


            return services;
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            var options = new DashboardOptions
            { 
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false, // Cambia a true en producción
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new[]
                        {
                            new BasicAuthAuthorizationUser
                            {
                                Login = "admin",
                                PasswordClear = "password" // Cambia esto en producción
                            }
                        }
                    })
                }
            };

            // Usar el dashboard de Hangfire
            app.UseHangfireDashboard("/hangfire", options);

            // Encolar un trabajo de ejemplo
            BackgroundJob.Enqueue(() => Console.WriteLine("Hello, Hangfire with MongoDB!"));
        }
    }
}
