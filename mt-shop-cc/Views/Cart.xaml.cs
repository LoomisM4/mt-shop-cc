using System;
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

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // TODO
        }

        private void Button_OnPressed(object sender, EventArgs e)
        {
            // TODO
        }
    }
}
