using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Sepidrah.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static readonly string ApiKey = "Sepidrah_UWP";
        public static readonly string APIHash = "53d6754fb254783d26f8b3224c6719897159420aa33ab3e439306af8c1e9c464";
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {

                    
                    SolidColorBrush Foreground = new SolidColorBrush(Colors.Black);
                    SolidColorBrush a = new SolidColorBrush(Colors.White);
                    try
                    {
                        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                        titleBar.BackgroundColor = a.Color;

                        titleBar.ButtonBackgroundColor = a.Color;

                        titleBar.InactiveBackgroundColor = a.Color;
                        titleBar.ButtonInactiveBackgroundColor = a.Color;
                        titleBar.InactiveForegroundColor = Foreground.Color;
                        titleBar.ButtonInactiveForegroundColor = Foreground.Color;
                        titleBar.ForegroundColor = Foreground.Color;
                        titleBar.ButtonForegroundColor = Foreground.Color;


                        //fuck you asshilism

                    }
                    catch
                    {

                    }
                    if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                    {
                        var statusBar = StatusBar.GetForCurrentView();
                        if (statusBar != null)
                        {
                            statusBar.BackgroundOpacity = 1;
                            statusBar.BackgroundColor = a.Color;
                            statusBar.ForegroundColor = Foreground.Color;
                        }
                    }
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    if (ApplicationData.Current.LocalSettings.Values["logged"] == null)
                        rootFrame.Navigate(typeof(Views.Welcome), e.Arguments);
                    else if ((bool)ApplicationData.Current.LocalSettings.Values["logged"]==true )
                        rootFrame.Navigate(typeof(Views.BasePage), e.Arguments);
                    else
                        rootFrame.Navigate(typeof(Views.Welcome), e.Arguments);

                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
