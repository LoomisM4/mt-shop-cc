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

    public List<CartArticle> Articles = new List<CartArticle>();
    public double TotalPrice { get; set; }

    public void AddArticle(Article article)
    {
        var already = Articles.Find(a => a.Article.Id == article.Id);
        if (already != null)
        {
            already.More();
        }
        else
        {
            var cartArticle = new CartArticle(article);
            Articles.Add(cartArticle);
        }
        CalcTotalPrice();
    }

    public void RemoveArticle(Article article)
    {
        var already = Articles.Find(a => a.Article.Id == article.Id);
        if (already != null && already.Quantity > 0)
        {
            already.Less();
        }
        CalcTotalPrice();
    }

    public void Clear()
    {
        Articles.Clear();
        CalcTotalPrice();
    }

    public void CalcTotalPrice()
    {
        double price = 0;
        Articles.ForEach(a => price = price + a.PositionPrice);
        TotalPrice = price;
    }

    public class CartArticle
    {
        public Article Article { get; set; }
        public int Quantity { get; set; }
        public double PositionPrice { get; set; }
        

        public CartArticle(Article article)
        {
            this.Article = article;
            this.Quantity = 1;
            CalcPositionPrice();
        }

        public void CalcPositionPrice()
        {
            PositionPrice = Article.Price * Quantity;
        }

        public void More()
        {
            Quantity++;
            CalcPositionPrice();
        }

        public void Less()
        {
            Quantity--;
            CalcPositionPrice();
        }
    }
}