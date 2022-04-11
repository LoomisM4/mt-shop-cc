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
    public partial class Categories : ContentPage
    {
        public ObservableCollection<Category> Items { get; set; }

        public Categories()
        {
            InitializeComponent();

            Items = new ObservableCollection<Category> {};

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // TODO
        }

        protected override void OnAppearing()
        {
            Api.Categories(null).ContinueWith(task =>
            {
                var result = task.Result;
                for (int i = 0; i < result.Length; i++)
                {
                    Items.Add(result[i]);
                }
            });
            base.OnAppearing();
        }
    }
}
