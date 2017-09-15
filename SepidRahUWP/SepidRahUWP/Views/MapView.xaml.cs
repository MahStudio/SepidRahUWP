using MappingUtilities;
using MappingUtilities.Geofencing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalMapControl.Tiles;
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

namespace SepidRahUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapView : Page
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

        public MapView()
        {
            //GoogleMap.DataSource = new Windows.UI.Xaml.Controls.Maps.HttpMapTileDataSource("http://mt1.google.com/vt/lyrs=m&amp;x={x}&amp;y={y}&amp;z={zoomlevel}");
            this.InitializeComponent();
            this.Loaded += MapView_Loaded;
            Map.MapServiceToken = "AIzaSyCS5gpejHZIpgK7StAfFCcTqZ8cQsuHVnw";
            //Map.TileSources.Add(new MapTileSource() { DataSource = new HttpMapTileDataSource("http://mt1.google.com/vt/lyrs=m&amp;x={x}&amp;y={y}&amp;z={z};" + "?key=AIzaSyCS5gpejHZIpgK7StAfFCcTqZ8cQsuHVnw") });
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                    // Carry out the operation.
                    //Geoposition pos = await geolocator.GetGeopositionAsync();

                    //Geopoint myPoint = new Geopoint(new BasicGeoposition() { Latitude = pos.Coordinate.Latitude, Longitude = pos.Coordinate.Longitude });
                    //MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "My position" };
                    //// add to map and center it
                    //Map.MapElements.Add(myPOI);
                    //Map.Center = myPoint;
                    var sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///DataSampleProvider/json.txt", UriKind.RelativeOrAbsolute));
                    var st = await FileIO.ReadTextAsync(sf);
                    var lst = JsonConvert.DeserializeObject<List<StationInfo>>(st);
                    var pinUri = new Uri("ms-appx:///Assets/LockScreenLogo.scale-200.png");
                    var lstpins = new List<PointOfInterest>();
                    var pinsf = await StorageFile.GetFileFromApplicationUriAsync(pinUri);
                    foreach (var item in lst)
                    {
                        Map.MapElements.Add(new MapIcon()
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

        private void mapItemButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
