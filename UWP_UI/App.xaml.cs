using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWP_UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public static BackgroundTaskDeferral AppServiceDeferral = null;
        public static AppServiceConnection Connection = null;
        
        public App()
        {
            //Set a breakpoint here and you will see it is called twice
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        public static async Task<string> TestConnection()
        {
            var result = "";

            ValueSet request = new ValueSet();
            request.Add("TestConnection", "");

            if (Connection == null)
            {
                result = " No Connection ";
                return result;
            }
            else
            {
                AppServiceResponse testResponse = await Connection.SendMessageAsync(request);
                foreach (var key in testResponse.Message.Keys)
                {
                    if (key.Equals("TestConnection"))
                    {
                        result = testResponse.Message["TestConnection"].ToString();
                        return result;
                    }
                }
                result = "No response";
            }
            
            return result;
        }
       
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            SetUp();
        }
        protected override void OnActivated(IActivatedEventArgs args)
        {
            SetUp();
        }

        private void SetUp()
        {
            LaunchFullTrustExtension();
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }

                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), null);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            
        }

   
        private async void LaunchFullTrustExtension()
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
        }
        protected async override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            base.OnBackgroundActivated(args);
            if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails details)
            {
                // only accept connections from callers in the same package
                if (details.CallerPackageFamilyName == Package.Current.Id.FamilyName)
                {
                    AppServiceDeferral = args.TaskInstance.GetDeferral();
                    Connection = details.AppServiceConnection;

                    var testResult = await App.TestConnection();
                    //Here the connection is ok.
                    //If you test the connection from the UI's button, you will see that connection is null.
                    //This is because this class is instanciated twice

                }
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
