using HangFireApi.Model;
using MongoDB.Driver;

namespace HangFireApi.Seed
{
    public static class EmpleadoSeed
    {
        private const string DATABASE_NAME = "traffig_dev";

        public static async Task GetEmpleados(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(DATABASE_NAME);
            var _empleadosCollection = mongoDatabase.GetCollection<Empleado>(typeof(Empleado).Name);
            var empleados = await _empleadosCollection.FindAsync(e => true);
            var count = await _empleadosCollection.EstimatedDocumentCountAsync();
            if (count > 0)
            {
                return; // Collection already contains data, no need to seed
            }
            var empleado = new List<Empleado>
        {
            new Empleado
            {
                Nombre = "Empleado 1",
                HoraDeInicio = new DateTime(2024, 6, 18, 15, 36, 11, DateTimeKind.Utc),
                HoraFinal = new DateTime(2024, 6, 18, 17, 36, 11, DateTimeKind.Utc),
                EstaActivo = true,
                FechaNacimiento = new DateTime(1990, 1, 1, 8, 30, 0, DateTimeKind.Utc)
            },
            new Empleado
            {
                Nombre = "Empleado 2",
                HoraDeInicio = new DateTime(2024, 6, 18, 10, 0, 0, DateTimeKind.Utc),
                HoraFinal = new DateTime(2024, 6, 18, 18, 0, 0, DateTimeKind.Utc),
                EstaActivo = true,
                FechaNacimiento = new DateTime(1985, 5, 15, 14, 0, 0, DateTimeKind.Utc)
            },
            new Empleado
            {
                Nombre = "Empleado 3",
                HoraDeInicio = new DateTime(2024, 6, 18, 9, 0, 0, DateTimeKind.Utc),
                HoraFinal = new DateTime(2024, 6, 18, 12, 0, 0, DateTimeKind.Utc),
                EstaActivo = false,
                FechaNacimiento = new DateTime(2000, 12, 25, 19, 45, 0, DateTimeKind.Utc)
            }
        };

            await _empleadosCollection.InsertManyAsync(empleado);
        }
    }
}
