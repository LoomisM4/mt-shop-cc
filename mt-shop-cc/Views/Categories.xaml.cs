using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuickType;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views // 1
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // 2
    public partial class Categories : ContentPage, IQueryAttributable // 1
    {
        public ObservableCollection<Category> Items { get; set; } // 1
        private Uri Url { get; set; } // 1

        public Categories() // 1
        {
            InitializeComponent(); // 1

            Items = new ObservableCollection<Category> {}; // 2
            
            MyListView.ItemsSource = Items; // 2
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query) // 1
        {
            Url = new Uri(query["url"]); // 3
            Title = query["title"]; // 2
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e) // 1
        {
            Category c = (Category) e.Item; // 3
            if (c.Links.Subcategories != null) // 4
            {
                await Shell.Current.GoToAsync($"subcategories?url={c.Links.Subcategories.Href}&title={c.Name}"); // 6
            } 
            else if (c.Links.Articles != null) // 4
            {
                await Shell.Current.GoToAsync($"articles?url={c.Links.Articles.Href}&title={c.Name}"); // 6
            }
        }

        protected override void OnAppearing() // 1
        {
            if (Title == null) // 2
            {
                Title = "Kategorien"; // 1
            }
            Items.Clear(); // 1
            LoadCategories(); // 1
            base.OnAppearing(); // 1
        }

        private void LoadCategories() // 1
        {
            Api.Categories(Url).ContinueWith(task => // 2
            {
                var result = task.Result; // 2
                foreach (var c in result) // 2
                {
                    Items.Add(c); // 1
                }
            });
        }
    }
}

// 57
