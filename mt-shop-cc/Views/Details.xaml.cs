using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using mt_shop_cc.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage, IQueryAttributable
    {
        private Uri Url { get; set; }
        
        public DetailsModel Model { get; set; }
        
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            this.Url = new Uri(query["url"]);
        }

        public Details()
        {
            InitializeComponent();

            Model = new DetailsModel();

            View.BindingContext = Model;
        }

        protected override void OnAppearing()
        {
            if (Url != null)
            {
                var task = Api.ArticleDetails(Url);
                task.ContinueWith(r =>
                {
                    var article = r.Result;
                    foreach (var link in article.Links.Images)
                    {
                        Model.Images.Add(Api.Img(link.Href));
                    }
                    Model.Article = article;
                });
            }
            base.OnAppearing();
        }

        private void Button_OnClicked(object sender, EventArgs e) {
            CartObj.Shared().AddArticle(Model.Article);
        }
    }
}
