using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views // 1
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // 2
    public partial class Spotlight : ContentPage // 1
    {
        public ObservableCollection<SpotlightModel> Items { get; set; } // 1

        public Spotlight() // 1
        {
            InitializeComponent(); // 1

            Items = new ObservableCollection<SpotlightModel> {}; // 2

            MyListView.ItemsSource = Items; // 2
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e) // 1
        {
            SpotlightModel model = (SpotlightModel) e.Item; // 3
            if (model.Article.Links.Details != null) // 5
            {
                await Shell.Current.GoToAsync($"details?url={model.Article.Links.Details.Href}&title={model.Article.Name}"); // 8
            }
        }

        protected override void OnAppearing() // 1
        {
            var task = Api.Spotlight(); // 2
            task.ContinueWith((r) => // 1
            {
                var articles = r.Result; // 2
                Items.Clear(); // 1
                foreach (var a in articles) // 2
                {
                    var model = new SpotlightModel // 2
                    {
                        Article = a, // 1
                        Image = Api.Img(a.Links.SpotlightImage.Href) // 5
                    };
                    Items.Add(model); // 1
                }
            });
            
            base.OnAppearing(); // 1
        }
    }
}

// 47