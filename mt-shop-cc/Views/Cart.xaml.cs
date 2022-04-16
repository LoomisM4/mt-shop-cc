using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using mt_shop_cc.ViewModels;
using QuickType;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        public CartModel Model { get; set; }

        public Cart()
        {
            InitializeComponent();

            Model = new CartModel();

            MyListView.ItemsSource = Model.Articles;
            BottomLayout.BindingContext = Model;
        }

        protected override void OnAppearing()
        {
            Model.Update();
            MyListView.ItemsSource = Model.Articles;
            BottomLayout.BindingContext = Model;
            base.OnAppearing();
        }

        private async void LessClicked(object sender, EventArgs e)
        {
            var param = ((TappedEventArgs) e).Parameter;
            Model.Less((Article) param);
        }

        private async void MoreClicked(object sender, EventArgs e)
        {
            var param = ((TappedEventArgs) e).Parameter;
            Model.More((Article) param);
        }

        private async void Button_OnPressed(object sender, EventArgs e)
        {
            Model.Clear();
            await DisplayAlert ("Erfolg", "Die Bestellung wurde abgeschickt", "OK");
        }
    }
}
