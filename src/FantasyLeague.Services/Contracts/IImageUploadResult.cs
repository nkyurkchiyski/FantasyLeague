namespace FantasyLeague.Services.Contracts
{
    public interface IImageUploadResult
    {
        string PublicId { get; }

        string Url { get; }
    }
}
