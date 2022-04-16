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
        private Uri Url { get; set; }

        public Categories()
        {
            InitializeComponent();

            Items = new ObservableCollection<Category> {};
            
            MyListView.ItemsSource = Items;
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            Url = new Uri(query["url"]);
            Title = query["title"];
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Category c = (Category) e.Item;
            if (c.Links.Subcategories != null)
            {
                await Shell.Current.GoToAsync($"subcategories?url={c.Links.Subcategories.Href}&title={c.Name}");
            } 
            else if (c.Links.Articles != null)
            {
                await Shell.Current.GoToAsync($"articles?url={c.Links.Articles.Href}&title={c.Name}");
            }
        }

        protected override void OnAppearing()
        {
            if (Title == null)
            {
                Title = "Kategorien";
            }
            Items.Clear();
            LoadCategories();
            base.OnAppearing();
        }

        private void LoadCategories()
        {
            Api.Categories(Url).ContinueWith(task =>
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
