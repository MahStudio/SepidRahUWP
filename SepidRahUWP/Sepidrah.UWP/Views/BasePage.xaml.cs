using PubSub;
using Sepidrah.UWP.Views.BaseSubpages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Sepidrah.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasePage : Page
    {
        public BasePage()
        {
            this.InitializeComponent();
            this.Subscribe<string>(Text =>
            {
                WhereIam.Text = Text;
            });
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            #region navigation

            Home.Navigate(typeof(Home));
            Weather.Navigate(typeof(Weather));
            Map.Navigate(typeof(Map));
            Star.Navigate(typeof(Suggestion));
            #endregion
        }

        #region Pivot
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // await Task.Delay(20);
            if (Pivot.SelectedIndex == 0)
            {
                mhome();

            }
            if (Pivot.SelectedIndex == 1)
            {
                mtoday();

            }
            if (Pivot.SelectedIndex == 2)
            {
                mmonth();


            }
            if (Pivot.SelectedIndex == 3)
            {
                mpref();

            }

        }
        private async void mhome()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 2);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            this.Publish("خانه");

            Pivot.SelectedIndex = 0;


        }
        private void mtoday()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 2);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            this.Publish("نقشه");
            Pivot.SelectedIndex = 1;


        }
        private void mmonth()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 2);
            bpref.BorderThickness = new Thickness(0, 0, 0, 0);
            this.Publish("آب و هوا");
            Pivot.SelectedIndex = 2;
        }
        private void mpref()
        {
            bhome.BorderThickness = new Thickness(0, 0, 0, 0);
            btoday.BorderThickness = new Thickness(0, 0, 0, 0);
            bmonth.BorderThickness = new Thickness(0, 0, 0, 0);
            bpref.BorderThickness = new Thickness(0, 0, 0, 2);
            this.Publish("پیشنهادات");
            Pivot.SelectedIndex = 3;

        }
        private void bhome_Click(object sender, RoutedEventArgs e)
        {
            mhome();
        }

        private void btoday_Click(object sender, RoutedEventArgs e)
        {
            mtoday();
        }

        private void bmonth_Click(object sender, RoutedEventArgs e)
        {
            mmonth();
        }
        private void bpref_Click(object sender, RoutedEventArgs e)
        {
            mpref();
        }
        #endregion

        private async void Feedback_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:ngame1390@live.com?subject=Sepidrah_FeedBack&cc=mohsens22@outlook.com"));

        }
    }
}
