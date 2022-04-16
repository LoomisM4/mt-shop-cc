using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using mt_shop_cc.ViewModels;
using QuickType;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleList : ContentPage, IQueryAttributable
    {
        public ObservableCollection<ArticleListModel> Items { get; set; }
        
        private Uri Url { get; set; }

        public ArticleList()
        {
            InitializeComponent();

            Items = new ObservableCollection<ArticleListModel>();

            ArticlesList.ItemsSource = Items;
        }
        
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            Url = new Uri(query["url"]);
            Title = query["title"];
        }

        protected override void OnAppearing()
        {
            Items.Clear();
            var task = Api.Articles(Url);
            task.ContinueWith(r =>
            {
                Article[] articles = r.Result;
                foreach (var a in articles)
                {
                    ArticleListModel model = new ArticleListModel();
                    model.Article = a;
                    if (a.Links.Preview != null)
                    {
                        model.Image = Api.Img(a.Links.Preview.Href);
                    }
                    Items.Add(model);
                }
            });
            base.OnAppearing();
        }

        private async void ArticlesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var model = (ArticleListModel) e.CurrentSelection.First();
            if (model.Article.Links.Details != null)
            {
                await Shell.Current.GoToAsync($"details?url={model.Article.Links.Details.Href}&title={model.Article.Name}");
            }
        }
    }
}
