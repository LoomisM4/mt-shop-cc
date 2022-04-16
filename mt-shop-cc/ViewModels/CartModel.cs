using System.Collections.ObjectModel;

namespace mt_shop_cc.ViewModels
{
    public class CartModel
    {
        public ObservableCollection<CartObj.CartArticle> Articles { get; set; }
        public double TotalPrice { get; set; }

        public CartModel()
        {
            Articles = new ObservableCollection<CartObj.CartArticle>();
            FillModel();
        }

        public void Update()
        {
            Articles.Clear();
            FillModel();
        }

        private void FillModel()
        {
            CartObj.Shared().Articles.ForEach(a => Articles.Add(a));
            TotalPrice = CartObj.Shared().TotalPrice;
        }
    }
}