using HangFireApi.Model;
using MongoDB.Driver;

namespace HangFireApi.Service;

public class ProgramacionRespository
{
    private readonly IMongoCollection<Programacion> _empleadosCollection;
    private const string DATABASE_NAME = "appimotion_dev";
    public ProgramacionRespository(IMongoClient mongoClient)
    {
        var mongoDatabase = mongoClient.GetDatabase(DATABASE_NAME);
        _empleadosCollection = mongoDatabase.GetCollection<Programacion>(typeof(Programacion).Name);
    }

    public async Task<List<Programacion>> GetEmpleadosAsync()
    {
        var empleados = await _empleadosCollection.FindAsync(e => true);
        return await empleados.ToListAsync();
    }

    public async Task FInd(FilterDefinition<Programacion> filter) =>
        await _empleadosCollection.FindAsync(filter);

    public async Task UpdateEmpleadoAsync(string id, Programacion empleadoIn) =>
        await _empleadosCollection.ReplaceOneAsync(e => e.Id == id, empleadoIn);

    public async Task InsertOneAsync(Programacion empleado)
    {
        await _empleadosCollection.InsertOneAsync(empleado);
    }
}
