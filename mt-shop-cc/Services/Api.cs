using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using QuickType;
using Xamarin.Forms;

public class Api
{
    private static string BaseUrl = "https://shop.marcelwettach.eu";

    public static async Task<Article[]> Spotlight()
    {
        string json = await CacheOrWeb(new Uri(BaseUrl + "/spotlight"));
        Response response = Response.FromJson(json);
        return response.Embedded.Articles;
    }

    public static async Task<Category[]> Categories(Uri url)
    {
        if (url == null)
        {
            url = new Uri(BaseUrl + "/categories");
        }
        string json = await CacheOrWeb(url);
        Response response = Response.FromJson(json);
        return response.Embedded.Categories;
    }

    public static async Task<Article[]> Articles(Uri url)
    {
        string json = await CacheOrWeb(url);
        Response response = Response.FromJson(json);
        return response.Embedded.Articles;
    }

    public static async Task<Article> ArticleDetails(Uri url)
    {
        string json = await CacheOrWeb(url);
        return Article.FromJson(json);
    }

    public static ImageSource Img(Uri url)
    {
        var source = ImageSource.FromUri(url);
        return source;
    }

    private static async Task<string> CacheOrWeb(Uri url)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            // TODO caching
            return json;
        } else
        {
            // TODO aus Cache
            return null;
        }
    }
}