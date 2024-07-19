using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HangFireApi.Model
{
    public class Empleado
    {
        public Empleado()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("horaDeInicio")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime HoraDeInicio { get; set; }

        [BsonElement("horaFinal")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime HoraFinal { get; set; }

        [BsonElement("estaActivo")]
        public bool EstaActivo { get; set; }

        [BsonElement("FechaNacimiento")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaNacimiento { get; set; }
    }

}
