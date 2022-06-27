using System;
using mt_shop_cc.ViewModels;
using QuickType;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views // 1
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // 2
    public partial class Cart : ContentPage // 1
    {
        public CartModel Model { get; set; } // 1

        public Cart() // 1
        {
            InitializeComponent(); // 1

            Model = new CartModel(); // 2

            MyListView.ItemsSource = Model.Articles; // 3
            BottomLayout.BindingContext = Model; // 2
        }

        protected override void OnAppearing() // 1
        {
            Model.Update(); // 1
            MyListView.ItemsSource = Model.Articles; // 3
            BottomLayout.BindingContext = Model; // 2
            base.OnAppearing(); // 1
        }

        private async void LessClicked(object sender, EventArgs e) // 1
        {
            var param = ((TappedEventArgs) e).Parameter; // 3
            Model.Less((Article) param); // 2
        }

        private async void MoreClicked(object sender, EventArgs e) // 1
        {
            var param = ((TappedEventArgs) e).Parameter; // 3
            Model.More((Article) param); // 2
        }

        private async void Button_OnPressed(object sender, EventArgs e) // 1
        {
            Model.Clear(); // 1
            await DisplayAlert ("Erfolg", "Die Bestellung wurde abgeschickt", "OK"); // 1
        }
    }
}

// 37