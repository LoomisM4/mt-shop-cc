using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using mt_shop_cc.ViewModels;
using QuickType;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views // 1
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // 2
    public partial class ArticleList : ContentPage, IQueryAttributable // 1
    {
        public ObservableCollection<ArticleListModel> Items { get; set; } // 1
        
        private Uri Url { get; set; } // 1

        public ArticleList() // 1
        {
            InitializeComponent(); // 1

            Items = new ObservableCollection<ArticleListModel>(); // 2

            ArticlesList.ItemsSource = Items; // 2
        }
        
        public void ApplyQueryAttributes(IDictionary<string, string> query) // 1
        {
            Url = new Uri(query["url"]); // 3
            Title = query["title"]; // 2
        }

        protected override void OnAppearing() // 1
        {
            Items.Clear(); // 1
            var task = Api.Articles(Url); // 2
            task.ContinueWith(r => // 1
            {
                Article[] articles = r.Result; // 2
                foreach (var a in articles) // 2
                {
                    ArticleListModel model = new ArticleListModel(); // 2
                    model.Article = a; // 2
                    if (a.Links.Preview != null) // 4
                    {
                        model.Image = Api.Img(a.Links.Preview.Href); // 6
                    }
                    Items.Add(model); // 1
                }
            });
            base.OnAppearing(); // 1
        }

        private async void ArticlesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) // 1
        {
            var model = (ArticleListModel) e.CurrentSelection.First(); // 4
            if (model.Article.Links.Details != null) // 5
            {
                await Shell.Current.GoToAsync($"details?url={model.Article.Links.Details.Href}&title={model.Article.Name}"); // 8
            }
        }
    }
}

// 61