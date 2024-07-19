using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace HangFireApi.Request;

public class ProgramacionRequest
{
    public string Name { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public List<DaysAvailableRouteRequest> DaysAvailableRoute { get; set; }
}

public partial class DaysAvailableRouteRequest
{
    public DayOfWeek Day { get; set; }
    public string TimeStart { get; set; }
    public string TimeEnd { get; set; }

    public bool IsActive { get; set; }
}