using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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

namespace SepidRahUWP.Views.RegisterView
{
    public class RegisterResponse
    {
        public int status { get; set; }
        public object[] error { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string token { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public string email { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterP1 : Page
    {
        public RegisterP1()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async delegate
            {
                var http = new HttpClient();
                http.DefaultRequestHeaders.Accept
      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("apikey", App.ApiKey);
                http.DefaultRequestHeaders.Add("apihash", App.APIHash);
                var res = await http.PostAsync("https://xugros.net/V0/auth/signup", new StringContent(JsonConvert.SerializeObject(new { email = EmailBox.Text, name = NameBox.Text, lastname = LastNameBox.Text, pass = PasswordBox.Password }), Encoding.UTF8, "application/json"));
                string result = res.Content.ReadAsStringAsync().Result;
                var jr = JsonConvert.DeserializeObject<RegisterResponse>(result);
                switch (jr.status)
                {
                    case 0:
                        ApplicationData.Current.LocalSettings.Values["Token"] = jr.data.token;
                        ApplicationData.Current.LocalSettings.Values["Email"] = jr.data.user.email;
                        ApplicationData.Current.LocalSettings.Values["Name"] = jr.data.user.name;
                        ApplicationData.Current.LocalSettings.Values["Lastname"] = jr.data.user.lastname;
                        Frame.BackStack.Clear();
                        Frame.Navigate(typeof(MainPage));
                        break;
                    case 1:
                        await new MessageDialog("1 ?").ShowAsync();
                        break;
                    case 2:
                        await new MessageDialog("Unexpected error").ShowAsync();
                        break;
                    default:
                        await new MessageDialog("Unexpected error").ShowAsync();
                        break;
                }
            });
        }
    }
}
