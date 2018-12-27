using FantasyLeague.Services.Contracts;

namespace FantasyLeague.Services
{
    public class ServiceResult : IServiceResult
    {
        public bool Success { get; set; }

        public string Error { get; set; }
    }
}
