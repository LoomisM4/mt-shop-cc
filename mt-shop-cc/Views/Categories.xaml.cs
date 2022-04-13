using System;
using System.Collections.Generic;
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
    public partial class Categories : ContentPage, IQueryAttributable
    {
        public ObservableCollection<Category> Items { get; set; }
        private Uri url { get; set; }

        public Categories()
        {
            InitializeComponent();

            Items = new ObservableCollection<Category> {};

            MyListView.ItemsSource = Items;
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            this.url = new Uri(query["url"]);
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Category c = (Category) e.Item;
            if (c.Links.Subcategories != null)
            {
                await Shell.Current.GoToAsync("subcategories?url={c.Links.Subcategories.Href}");
            } else if (c.Links.Articles != null)
            {
                await Shell.Current.GoToAsync("articles?url={c.Links.Articles.Href}");
            }
        }

        protected override void OnAppearing()
        {
            Items.Clear();
            LoadCategories();
            base.OnAppearing();
        }

        private void LoadCategories()
        {
            Api.Categories(url).ContinueWith(task =>
            {
                var result = task.Result;
                foreach (var c in result)
                {
                    Items.Add(c);
                }
            });
        }
    }
}
