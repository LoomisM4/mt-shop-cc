using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuickType;
using Xamarin.Forms;

namespace mt_shop_cc.ViewModels // 1
{
    public class DetailsModel : INotifyPropertyChanged // 1
    {
        private Article _article; // 0
        public Article Article // 1
        {
            get => _article; // 1
            set
            {
                _article = value; // 1
                OnPropertyChanged("Article"); // 1
            }
        }

        public ObservableCollection<ImageSource> Images { get; set; } // 1

        public DetailsModel () // 1
        {
            Images = new ObservableCollection<ImageSource>(); // 2
        }


        public event PropertyChangedEventHandler PropertyChanged; // 1

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) // 1
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // 2
        }
    }
}

// 14