using System.Collections.Generic;
using QuickType;

public class CartObj // 1
{
    private static readonly CartObj Cart = new CartObj(); // 2
    
    private CartObj() {} // 1

    public static CartObj Shared() // 1
    {
        return Cart; // 1
    }

    public List<CartArticle> Articles = new List<CartArticle>(); // 2
    public double TotalPrice { get; set; } // 1

    public void AddArticle(Article article) // 1
    {
        var already = Articles.Find(a => a.Article.Id == article.Id); // 6
        if (already != null) // 2
        {
            already.More(); // 1
        }
        else // 1
        {
            var cartArticle = new CartArticle(article); // 2
            Articles.Add(cartArticle); // 1
        }
        CalcTotalPrice(); // 1
    }

    public void RemoveArticle(Article article) // 1
    {
        var already = Articles.Find(a => a.Article.Id == article.Id); // 6
        if (already != null && already.Quantity > 0) // 5
        {
            already.Less(); // 1
        }
        CalcTotalPrice(); // 1
    }

    public void Clear() // 1
    {
        Articles.Clear(); // 1
        CalcTotalPrice(); // 1
    }

    public void CalcTotalPrice() // 1
    {
        double price = 0; // 1
        Articles.ForEach(a => price += a.PositionPrice); // 3
        TotalPrice = price; // 1
    }

    public class CartArticle // 1
    {
        public Article Article { get; set; } // 1
        public int Quantity { get; set; } // 1
        public double PositionPrice { get; set; } // 1
        

        public CartArticle(Article article) // 1
        {
            Article = article; // 1
            Quantity = 1; // 1
            CalcPositionPrice(); // 1
        }

        public void CalcPositionPrice() // 1
        {
            PositionPrice = Article.Price * Quantity; // 3
        }

        public void More() // 1
        {
            Quantity++; // 1
            CalcPositionPrice(); // 1
        }

        public void Less() // 1
        {
            Quantity--; // 1
            CalcPositionPrice(); // 1
        }
    }
}

// 65