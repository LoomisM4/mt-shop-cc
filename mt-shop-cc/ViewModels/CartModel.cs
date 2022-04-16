using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        private void FillModel()
        {
            CartObj.Shared().Articles.ForEach(a => Articles.Add(a));
            TotalPrice = CartObj.Shared().TotalPrice;
            OnPropertyChanged("TotalPrice");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}