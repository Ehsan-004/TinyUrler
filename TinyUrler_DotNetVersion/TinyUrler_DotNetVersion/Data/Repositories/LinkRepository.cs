using Microsoft.EntityFrameworkCore;
using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.Models.Context;



namespace TinyUrler_DotNetVersion.Data
{
    public class LinkRepository : IlinkRepository
    {
        private readonly TContext _context;

        public LinkRepository(TContext context)
        {
            _context = context;
        }

        /// <summary> Used to get all links </summary>
        /// <returns> A list of links </returns>
        public async Task<IEnumerable<Link>> GetLinksAsync()
        {
            return await _context.Links.ToListAsync();
        }

        public IEnumerable<Link> GetLinks()
        {
            return _context.Links.ToList();
        }

        /// <summary> Used to get a link by id </summary>
        /// <param name="id"></param>
        /// <returns> A link if exists, otherwise null </returns>
        public Task<Link> GetLinkByIdAsync(int id)
        {
            return _context.Links.FirstOrDefaultAsync(l => l.Id == id);
        }

        /// <summary> Receives a short url and retrurns the linked link </summary>
        /// <param name="short_url"> A string , short url </param> 
        /// <returns> A link if exists, otherwise null </returns>
        public Link GetLinkByShortUrlAsync(string short_url)
        {
            return _context.Links.FirstOrDefault(l => l.ShortUrl == short_url);
        }

        public string GetShortUrlByLink(string link)
        {
            return _context.Links.FirstOrDefault(l => l.Url.Trim() == link.Trim()).ShortUrl;
        }

        public bool UrlExists(string url)
        {
            var res = _context.Links.FirstOrDefault(x => x.Url == url);
            return true ? res != null : false;
        }

        public bool ShortUrlExist(string short_url)
        {
            var res = _context.Links.FirstOrDefault(x => x.ShortUrl == short_url);
            return true ? res != null : false;
        }

        public bool CreateLink(string url, string short_url)
        {
            var link = new Link()
            {
                Url=url,
                ShortUrl=short_url
            };
            _context.Add(link);
            return Save();
        }

        public bool DeleteLink(int id)
        {
            var link = GetLinkByIdAsync(id);
            _context.Remove(link);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return true ? saved > 0 : false;
        }

        public bool UpdateLink(string url, string short_url)
        {
            throw new NotImplementedException();
        }

        
    }
}
