using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using QuickType;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Spotlight : ContentPage
    {
        public ObservableCollection<Article> Items { get; set; }

        public Spotlight()
        {
            InitializeComponent();

            Items = new ObservableCollection<Article> {};

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // TODO
        }

        protected override void OnAppearing()
        {
            var task = Api.Spotlight();
            task.ContinueWith((r) =>
            {
                var articles = r.Result;
                Items.Clear();
                for (int i = 0; i < articles.Length; i++)
                {
                    Items.Add(articles[i]);
                }
            });
            
            base.OnAppearing();
        }
    }
}
