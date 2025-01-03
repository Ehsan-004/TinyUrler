using TinyUrler_DotNetVersion.Models;

namespace TinyUrler_DotNetVersion.Data
{
    public interface IlinkRepository
    {
        public Task<IEnumerable<Link>> GetLinksAsync();
        public IEnumerable<Link> GetLinks();
        public Task<Link> GetLinkByIdAsync(int id);
        public Link GetById(int id);
        public Link GetLinkByShortUrlAsync(string name);
        public string GetShortUrlByLink(string link);
        public  bool UrlExists(string url);
        public bool ShortUrlExist(string  short_url);
        public bool CreateLink(Link link);
        public bool CreateByData(string url, string short_url);
        public  bool UpdateLink(Link link);
        public bool DeleteLink(int id);
        public bool Save();
    }
}