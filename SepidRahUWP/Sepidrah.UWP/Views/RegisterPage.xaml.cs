using Newtonsoft.Json;
using Sepidrah.UWP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sepidrah.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async delegate
            {
                var http = new HttpClient();
                http.DefaultRequestHeaders.Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("apikey", App.ApiKey);
                http.DefaultRequestHeaders.Add("apihash", App.APIHash);
                var res = await http.PostAsync("https://xugros.net/V0/auth/signup", new StringContent(JsonConvert.SerializeObject(new { email = email.Text, name = name.Text, lastname = lastname.Text, pass = _passBox.Password }), Encoding.UTF8, "application/json"));
                string result = res.Content.ReadAsStringAsync().Result;
                var jr = JsonConvert.DeserializeObject<ResponseBase>(result);
                switch (jr.Status)
                {
                    case Status.OK:
                        ApplicationData.Current.LocalSettings.Values["logged"] = true;
                        ApplicationData.Current.LocalSettings.Values["Token"] = (string)(jr.Data).token;
                        ApplicationData.Current.LocalSettings.Values["Email"] = (string)(jr.Data).email;
                        ApplicationData.Current.LocalSettings.Values["Name"] = (string)(jr.Data).name;
                        ApplicationData.Current.LocalSettings.Values["Lastname"] = (string)(jr.Data).lastname;
                        Frame.BackStack.Clear();
                        Frame.Navigate(typeof(BasePage));
                        break;
                    case Status.Faiure:
                        await new MessageDialog("Error: " + result).ShowAsync();
                        break;
                    
                    default:
                        await new MessageDialog("Unexpected error").ShowAsync();
                        break;
                }
            });
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
