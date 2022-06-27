using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using QuickType;
using Xamarin.Essentials;
using Xamarin.Forms;

public class Api // 1
{
    private static string BaseUrl = "https://shop.marcelwettach.eu"; // 1

    public static async Task<Article[]> Spotlight() // 1
    {
        string json = await CacheOrWeb(new Uri(BaseUrl + "/spotlight")); // 4
        Response response = Response.FromJson(json); // 2
        return response.Embedded.Articles; // 3
    }

    public static async Task<Category[]> Categories(Uri url) // 1
    {
        if (url == null) // 2
        {
            url = new Uri(BaseUrl + "/categories"); // 3
        }
        string json = await CacheOrWeb(url); // 2
        Response response = Response.FromJson(json); // 2
        return response.Embedded.Categories; // 3
    }

    public static async Task<Article[]> Articles(Uri url) // 1
    {
        string json = await CacheOrWeb(url); // 2
        Response response = Response.FromJson(json); // 2
        return response.Embedded.Articles; // 3
    }

    public static async Task<Article> ArticleDetails(Uri url) // 1
    {
        string json = await CacheOrWeb(url); // 2
        return Article.FromJson(json); // 2
    }

    public static ImageSource Img(Uri url) // 1
    {
        var source = new UriImageSource // 2
        {
            Uri = url, // 1
            CachingEnabled = true // 1
        };
        return source; // 1
    }

    private static async Task<string> CacheOrWeb(Uri url) // 1
    {
        HttpClient client = new HttpClient(); // 2
        String newUrlStr = url.AbsoluteUri.Replace("http://", "https://"); // 3
        Uri newUrl = new Uri(newUrlStr); // 2
        HttpResponseMessage response = await client.GetAsync(newUrl); // 2
        if (response.IsSuccessStatusCode) // 2
        {
            string json = await response.Content.ReadAsStringAsync(); // 3
            Preferences.Set(url.AbsolutePath, json); // 2
            return json; // 1
        }
        
        return Preferences.Get(url.AbsolutePath, null); // 3
    }
}

// 65