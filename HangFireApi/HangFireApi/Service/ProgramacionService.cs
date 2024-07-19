namespace HangFireApi.Service;

public class ProgramacionService
{
    private readonly ProgramacionRespository respository;

    public ProgramacionService(ProgramacionRespository respository)
    {
        this.respository = respository;
    }
}
