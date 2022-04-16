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
        public ObservableCollection<SpotlightModel> Items { get; set; }

        public Spotlight()
        {
            InitializeComponent();

            Items = new ObservableCollection<SpotlightModel> {};

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            SpotlightModel model = (SpotlightModel) e.Item;
            if (model.Article.Links.Details != null)
            {
                await Shell.Current.GoToAsync($"details?url={model.Article.Links.Details.Href}&title={model.Article.Name}");
            }
        }

        protected override void OnAppearing()
        {
            var task = Api.Spotlight();
            task.ContinueWith((r) =>
            {
                var articles = r.Result;
                Items.Clear();
                foreach (var a in articles)
                {
                    var model = new SpotlightModel
                    {
                        Article = a,
                        Image = Api.Img(a.Links.SpotlightImage.Href)
                    };
                    Items.Add(model);
                }
            });
            
            base.OnAppearing();
        }
    }
}
