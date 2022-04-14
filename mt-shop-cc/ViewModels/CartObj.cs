using System.Collections.Generic;
using QuickType;

public class CartObj
{
    private static readonly CartObj Cart = new CartObj();
    
    private CartObj() {}

    public static CartObj Shared()
    {
        return Cart;
    }

    private List<CartArticle> articles = new List<CartArticle>();

    public void AddArticle(Article article)
    {
        var already = articles.Find(a => a.article.Id == article.Id);
        if (already != null)
        {
            already.quantity++;
        } else
        {
            var cartArticle = new CartArticle(article);
            articles.Add(cartArticle);
        }
    }

    public void RemoveArticle(Article article)
    {
        var already = articles.Find(a => a.article.Id == article.Id);
        if (already != null && already.quantity > 0)
        {
            already.quantity--;
        }
    }

    public void Clear()
    {
        articles.Clear();
    }

    public double CalcTotalPrice()
    {
        double price = 0;
        articles.ForEach(a => price = price + a.CalcPositionPrice());
        return price;
    }

    private class CartArticle
    {
        public Article article;
        public int quantity;

        public CartArticle(Article article)
        {
            this.article = article;
            this.quantity = 1;
        }

        public double CalcPositionPrice()
        {
            return article.Price * quantity;
        }
    }
}