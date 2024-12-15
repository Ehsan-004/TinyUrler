using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime;
using System.Text;
using TinyUrler_DotNetVersion.Data;
using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.Models.Context;
using static System.Net.WebRequestMethods;

namespace TinyUrler_DotNetVersion.Controllers
{
    public static class Tools
    {
        private static readonly Random random = new Random();

        // charachters = "a list of characters including lowercase and numbers"
        private static readonly List<char> _alphabetNumericCharacters = new List<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };



        public static List<T> ShuffleList<T>(List<T> list)
        { 
            // AI generated code!
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static string GenerateShortId(int length = 6)
        {
            // Shuffle the list of characters
            var shuffledCharacters = Tools._alphabetNumericCharacters;
            Tools.ShuffleList(shuffledCharacters);


            ShuffleList(shuffledCharacters);

            // Generate a random string of characters
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(shuffledCharacters.Count);
                sb.Append(shuffledCharacters[index]);
            }

            return sb.ToString();
        }
    }

    public class HomeController : Controller
    {
        private readonly IlinkRepository _IlinkRepository;

        public HomeController(IlinkRepository ilinkRepository)
        {
            _IlinkRepository = ilinkRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult generate_short_url(string url)
        {
            if (_IlinkRepository.UrlExists(url))
                return Json(new { 
                    //error = true,
                    errorText = "This URL already exists!",
                    url = url,
                    ShortUrl = _IlinkRepository.GetShortUrlByLink(url),
                });

            string short_url = Tools.GenerateShortId();

            while (true)
            {
                if (!_IlinkRepository.ShortUrlExist(short_url))
                    break;
                short_url = Tools.GenerateShortId();
            }

            if (_IlinkRepository.CreateLink(url, short_url))
                return Json(new { 
                    //error = false,
                    success = true, 
                    url = url,
                    shortUrl = short_url ,
                    errorText = "Short link successfully created!"
                });

            return Json(new { 
                //error = true,
                errorText = "Failed to create short URL" ,
                shortUrl = "Couldn't create a short url."
            });
        }
        
        [Route("{shortUrl}")]
        public IActionResult LinkRedirect(string shortUrl)
        {
            var url = _IlinkRepository.GetLinkByShortUrlAsync(shortUrl);
            if (url == null)
            {
                return View("Index");
            }

            return Redirect(url.Url);
        }
        
        [Route("home/all/")]
        public IActionResult All()
        {
            return Json(new { links = _IlinkRepository.GetLinks() });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}