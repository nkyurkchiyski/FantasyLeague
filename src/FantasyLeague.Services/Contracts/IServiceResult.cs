namespace FantasyLeague.Services.Contracts
{
    public interface IServiceResult
    {
        bool Succeeded { get; }

        string Error { get; }
    }
}
