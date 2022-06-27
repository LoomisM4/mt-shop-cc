using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuickType;

namespace mt_shop_cc.ViewModels // 1
{
    public class CartModel : INotifyPropertyChanged // 1
    {
        public ObservableCollection<CartObj.CartArticle> Articles { get; } // 1
        public double TotalPrice { get; set; } // 1

        public CartModel() // 1
        {
            Articles = new ObservableCollection<CartObj.CartArticle>(); // 3
            FillModel(); // 1
        }

        public void Update() // 1
        {
            Articles.Clear(); // 1
            FillModel(); // 1
        }

        public void Clear() // 1
        {
            CartObj.Shared().Clear(); // 2
            Update(); // 1
        }

        public void More(Article article) // 1
        {
            CartObj.Shared().AddArticle(article); // 2
            Update(); // 1
        }

        public void Less(Article article) // 1
        {
            CartObj.Shared().RemoveArticle(article); // 2
            Update(); // 1
        }

        private void FillModel() // 1
        {
            CartObj.Shared().Articles.ForEach(a => Articles.Add(a)); // 4
            TotalPrice = CartObj.Shared().TotalPrice; // 3
            OnPropertyChanged("TotalPrice"); // 1
            OnPropertyChanged("Articles"); // 1
        }

        public event PropertyChangedEventHandler PropertyChanged; // 1

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) // 1
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // 2
        }
    }
}

// 38