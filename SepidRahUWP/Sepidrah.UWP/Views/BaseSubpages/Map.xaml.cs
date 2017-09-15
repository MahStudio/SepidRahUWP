using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sepidrah.UWP.Views.BaseSubpages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        public class PointOfInterest
        {
            public string DisplayName { get; set; }
            public Geopoint Location { get; set; }
            public Uri ImageSourceUri { get; set; }
            public Point NormalizedAnchorPoint { get; set; }
        }
        public class StationInfo
        {
            public int stationId { get; set; }
            public string stationName { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public string address { get; set; }
            public string region { get; set; }
        }
        public Map()
        {
            this.InitializeComponent();
            this.Loaded += MapView_Loaded;
            MyMap.MapServiceToken = "AIzaSyCS5gpejHZIpgK7StAfFCcTqZ8cQsuHVnw";
        }
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                    // Carry out the operation.
                    Geoposition pos = await geolocator.GetGeopositionAsync();

                    Geopoint myPoint = new Geopoint(new BasicGeoposition() { Latitude = pos.Coordinate.Latitude, Longitude = pos.Coordinate.Longitude });
                    MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "موقعیت شما" };
                    //// add to map and center it
                    MyMap.MapElements.Add(myPOI);
                    MyMap.Center = myPoint;
                    MyMap.ZoomLevel = 13;
                    MyMap.MapElementClick += MyMap_MapElementClick;
                    var sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///DataSampleProvider/json.txt", UriKind.RelativeOrAbsolute));
                    var st = await FileIO.ReadTextAsync(sf);
                    var lst = JsonConvert.DeserializeObject<List<StationInfo>>(st);
                    var pinUri = new Uri("ms-appx:///Assets/pin.png");
                    var lstpins = new List<PointOfInterest>();
                    var pinsf = await StorageFile.GetFileFromApplicationUriAsync(pinUri);
                    foreach (var item in lst)
                    {
                        MyMap.MapElements.Add(new MapIcon()
                        {
                            Image = RandomAccessStreamReference.CreateFromFile(pinsf),
                            Title = item.stationName,
                            Location = new Geopoint(new BasicGeoposition() { Latitude = item.latitude, Longitude = item.longitude }),
                        });
                    }
                    break;

                case GeolocationAccessStatus.Denied:
                    break;

                case GeolocationAccessStatus.Unspecified:
                    break;
            }
        }

        private async void MyMap_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///DataSampleProvider/json.txt"));
            var Txt = await FileIO.ReadTextAsync(sf);
            var sts = JsonConvert.DeserializeObject<List<StationInfo>>(Txt);
            float X =(float) args.Location.Position.Latitude;
            float Y = (float)args.Location.Position.Longitude;
            //Debug.WriteLine(sts.Where(x => x.latitude == X && x.longitude == Y).FirstOrDefault().address);
        }

        private void mapItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
