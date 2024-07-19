using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HangFireApi.Model;

public class Programacion
{
    public Programacion(string name, DateTime dateStart, DateTime dateEnd, List<DaysAvailableRoute> daysAvailableRoute)
    {
        Id = ObjectId.GenerateNewId().ToString();
        Name = name;
        DateStart = dateStart;
        DateEnd = dateEnd;
        DaysAvailableRoute = daysAvailableRoute;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DateStart { get; set; }
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DateEnd { get; set; }
    public List<DaysAvailableRoute> DaysAvailableRoute { get; set; }
}

public partial class DaysAvailableRoute
{
    public DaysAvailableRoute(DayOfWeek day, string timeStart, string timeEnd, bool isActive)
    {
        Day = day;
        TimeStart = TimeSpan.Parse(timeStart);
        TimeEnd = TimeSpan.Parse(timeEnd);
        IsActive = isActive;
    }

    public DayOfWeek Day { get; set; }
    public TimeSpan TimeStart { get; set; }
    public TimeSpan TimeEnd { get; set; }
    public string JobId { get; set; }
    public bool IsActive { get; set; }
}
