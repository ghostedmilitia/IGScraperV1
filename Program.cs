using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IGScraperV1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // receive a profile name
            var profileName = args[0];
            var url = $"https://instagram.com/{profileName}";
            Console.WriteLine($"The profile name is {profileName}");
            Console.WriteLine($"URL to be queried {url}");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var htmlBody = await response.Content.ReadAsStringAsync();
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(htmlBody);

                    var uselessString = "window._sharedData = ";
                    var scripts = htmlDocument.DocumentNode.SelectNodes("/html/body/script");
                    //var jsonString = scripts[0].InnerText
                    var scriptInnerText = scripts[0].InnerText.Substring(uselessString.Length)
                        .Replace(";", "");

                    dynamic jsonStuff = JObject.Parse(scriptInnerText);
                    Console.WriteLine(jsonStuff);
                    //Console.WriteLine($"Got cool array of scripts of size {scripts.Count}");
                }
            }
            // TODO: make everything asynchronous
            // query instagram using the profile name

            // parse the html

            // display some cool stuff.
            Console.WriteLine("information provided by: ghosted.php");
        }
    }
}
