using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;

class Program
{
    static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. IP Check");
            Console.WriteLine("2. Local time in Sofia");
            Console.WriteLine("3. Scrape news");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await GetCountryFromIP();
                    break;
                case "2":
                    await GetSofiaTime();
                    break;
                case "3":
                    await ScrapeNews();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid selection! Please, try again.");
                    break;
            }
        }
    }

    static async Task GetCountryFromIP()
    {
        Console.Write("Enter IP address: ");
        string ip = Console.ReadLine();
        string url = $"https://ipapi.co/{ip}/country/";

        using HttpClient client = new HttpClient();
        try
        {
            string country = await client.GetStringAsync(url);
            Console.WriteLine($"Country: {country.Trim()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in request: " + ex.Message);
        }
    }

    static async Task GetSofiaTime()
    {
        string url = "https://www.timeanddate.com/worldclock/bulgaria/sofia";
        using HttpClient client = new HttpClient();
        try
        {
            string html = await client.GetStringAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var timeNode = doc.DocumentNode.SelectSingleNode("//span[@id='ct']");
            if (timeNode != null)
            {
                Console.WriteLine("Local time in Sofia: " + timeNode.InnerText);
            }
            else
            {
                Console.WriteLine("Error fetching local time.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in request: " + ex.Message);
        }
    }

    static async Task ScrapeNews()
    {
        string url = "https://www.mediapool.bg/";
        using HttpClient client = new HttpClient();
        try
        {
            string html = await client.GetStringAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var articles = doc.DocumentNode.SelectNodes("//h3/a");
            if (articles != null && articles.Count > 0)
            {
                Console.WriteLine("Актуални новини:");
                List<string> filteredNews = new List<string>();
                foreach (var article in articles)
                {
                    string title = article.InnerText.Trim();
                    string lowerTitle = title.ToLower();
                    if (!lowerTitle.Contains("covid-19") && !lowerTitle.Contains("корона вирус") && !lowerTitle.Contains("пандемия"))
                    {
                        filteredNews.Add("- " + title);
                    }
                }

                if (filteredNews.Count > 0)
                {
                    foreach (var news in filteredNews)
                    {
                        Console.WriteLine(news);
                    }
                }
                else
                {
                    Console.WriteLine("No news found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in request: " + ex.Message);
        }
    }
}