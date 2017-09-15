using SepidRahUWP.Views;
using SepidRahUWP.Views.RegisterView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SepidRahUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        #region Default Definitions
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(ApiInformation.IsTypePresent("Windows.Phone.UI.Input"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
                rootframe.Navigated += Rootframe_Navigated;
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, delegate
            {
                rootframe.Navigate(typeof(LoginView));
            });
        }

        private void Rootframe_Navigated(object sender, NavigationEventArgs e)
        {
            var navman = SystemNavigationManager.GetForCurrentView();
            if (rootframe.CanGoBack)
                navman.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            else
                navman.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

        }

        private void MainPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if (rootframe.CanGoBack)
                rootframe.GoBack();
        }

        private async void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (rootframe.CanGoBack)
                rootframe.GoBack();
            else
            {
                var msg = new MessageDialog("Exit Sepid Rah ?");
                msg.Commands.Add(new UICommand("Yes", delegate
                {
                    Application.Current.Exit();
                }));
                msg.Commands.Add(new UICommand("No"));
                await msg.ShowAsync();
            }
        }
        #endregion

    }
}
