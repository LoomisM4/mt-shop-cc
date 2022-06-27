using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace mtshopcc.Views // 1
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // 2
    public partial class Map : ContentPage // 1
    {
        public Xamarin.Forms.Maps.Map MapObj; // 1
        public Map() // 1
        {
            InitializeComponent(); // 1
            MapObj = new Xamarin.Forms.Maps.Map(); // 4
            MapObj.IsShowingUser = true; // 2
            Content = MapObj; // 1
            
            GetStore(); // 1
        }

        private async void GetStore() // 1
        {
            var location = await Geolocation.GetLocationAsync(); // 2
            var pin = new Pin // 2
            {
                Label = "", // 1
                Position = new Position(location.Latitude, location.Longitude) // 4
            };
            MapObj.Pins.Add(pin); // 2
        }
    }
}

// 27