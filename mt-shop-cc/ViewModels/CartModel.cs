using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuickType;

namespace mt_shop_cc.ViewModels
{
    public class CartModel : INotifyPropertyChanged
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

        public void More(Article article)
        {
            CartObj.Shared().AddArticle(article);
            Update();
        }

        public void Less(Article article)
        {
            CartObj.Shared().RemoveArticle(article);
            Update();
        }

        private void FillModel()
        {
            CartObj.Shared().Articles.ForEach(a => Articles.Add(a));
            TotalPrice = CartObj.Shared().TotalPrice;
            OnPropertyChanged("TotalPrice");
            OnPropertyChanged("Articles");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}