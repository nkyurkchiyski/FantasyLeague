namespace FantasyLeague.Services.Contracts
{
    public interface IServiceResult
    {
        bool Success { get; }

        string Error { get; }
    }
}
