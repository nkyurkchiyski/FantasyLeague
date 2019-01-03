using FantasyLeague.Services.Contracts;

namespace FantasyLeague.Services
{
    public class ImageUploadResult : IImageUploadResult
    {
        public string PublicId { get; set; }

        public string Url { get; set; }
    }
}
