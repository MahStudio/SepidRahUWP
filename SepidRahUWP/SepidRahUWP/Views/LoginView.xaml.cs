using Newtonsoft.Json;
using SepidRahUWP.Views.RegisterView;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class LoginView : Page
    {
        public LoginView()
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
                var res = await http.PostAsync("https://xugros.net/V0/auth/signin", new StringContent(JsonConvert.SerializeObject(new { email = EmailBox.Text, pass = PasswordBox.Password }), Encoding.UTF8, "application/json"));
            });
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterP1));
        }
    }
}
