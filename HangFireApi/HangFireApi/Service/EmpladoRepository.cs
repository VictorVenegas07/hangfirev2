using HangFireApi.Model;
using MongoDB.Driver;

namespace HangFireApi.Service;

public class EmpladoRepository
{
    private readonly IMongoCollection<Empleado> _empleadosCollection;
    private const string DATABASE_NAME = "traffig_dev";
    public EmpladoRepository(IMongoClient mongoClient)
    {
        var mongoDatabase = mongoClient.GetDatabase(DATABASE_NAME);
        _empleadosCollection = mongoDatabase.GetCollection<Empleado>(typeof(Empleado).Name);
    }

    public async Task<List<Empleado>> GetEmpleadosAsync()
    {
        var empleados = await _empleadosCollection.FindAsync(e => true);
        return await empleados.ToListAsync();
    }

    public async Task FInd(FilterDefinition<Empleado> filter) =>
        await _empleadosCollection.FindAsync(filter);

    public async Task UpdateEmpleadoAsync(string id, Empleado empleadoIn) =>
        await _empleadosCollection.ReplaceOneAsync(e => e.Id == id, empleadoIn);

    public List<Empleado> BuscarPersonaPorFecha(DateTime fechaNacimiento)
    {
        var filter = Builders<Empleado>.Filter.Eq(p => p.FechaNacimiento, fechaNacimiento);
        return _empleadosCollection.Find(filter).ToList();
    }

    public List<Empleado> BuscarEmpleadosVigentes(DateTime ahoraUtc)
    {
        var filter = Builders<Empleado>.Filter.And(
            Builders<Empleado>.Filter.Lte(e => e.HoraDeInicio, ahoraUtc),
            Builders<Empleado>.Filter.Gte(e => e.HoraFinal, ahoraUtc),
            Builders<Empleado>.Filter.Eq(e => e.EstaActivo, true)
        );

        return _empleadosCollection.Find(filter).ToList();
    }

    public async Task InsertOneAsync(Empleado empleado)
    {
        await _empleadosCollection.InsertOneAsync(empleado);
    }
}
