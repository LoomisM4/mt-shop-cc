using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuickType;
using Xamarin.Forms;

namespace mt_shop_cc.ViewModels
{
    public class DetailsModel : INotifyPropertyChanged
    {
        private Article _article;
        public Article Article
        {
            get => _article;
            set
            {
                _article = value;
                OnPropertyChanged("Article");
            }
        }

        public ObservableCollection<ImageSource> Images { get; set; }

        public DetailsModel ()
        {
            Images = new ObservableCollection<ImageSource>();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}