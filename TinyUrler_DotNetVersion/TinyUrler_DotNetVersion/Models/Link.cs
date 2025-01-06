using TinyUrler_DotNetVersion.Models.Enum;

namespace TinyUrler_DotNetVersion.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public LinkType LinkType { get; set; } = LinkType.Normal;
        
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
