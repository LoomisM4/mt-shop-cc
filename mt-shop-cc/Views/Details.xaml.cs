using System;
using System.Collections.Generic;
using mt_shop_cc.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views // 1
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // 2
    public partial class Details : ContentPage, IQueryAttributable // 1
    {
        private Uri Url { get; set; } // 1
        
        public DetailsModel Model { get; set; } // 1
        
        public void ApplyQueryAttributes(IDictionary<string, string> query) // 1
        {
            Url = new Uri(query["url"]); // 3
            Title = query["title"]; // 2
        }

        public Details() // 1
        {
            InitializeComponent(); // 1

            Model = new DetailsModel(); // 2

            View.BindingContext = Model; // 2
        }

        protected override void OnAppearing() // 1
        {
            if (Url != null) // 2
            {
                var task = Api.ArticleDetails(Url); // 2
                task.ContinueWith(r => // 1
                {
                    var article = r.Result; // 2
                    foreach (var link in article.Links.Images) // 4
                    {
                        Model.Images.Add(Api.Img(link.Href)); // 4
                    }
                    Model.Article = article; // 2
                });
            }
            base.OnAppearing(); // 1
        }

        private void Button_OnClicked(object sender, EventArgs e) { // 1
            CartObj.Shared().AddArticle(Model.Article); // 3
        }
    }
}

// 41