using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace FullTrustExtension
{
    class Program
    {
        static AutoResetEvent done;
        public static AppServiceConnection Connection;
        static void Main(string[] args)
        {
            done = new AutoResetEvent(false);
            InitializeAppServiceConnection();
            done.WaitOne();
        }

        private static async void InitializeAppServiceConnection()
        {
            Connection = new AppServiceConnection();
            Connection.AppServiceName = "AppConnection";
            var familyName = Package.Current.Id.FamilyName;
            Connection.PackageFamilyName = familyName;
            Connection.RequestReceived += Connection_RequestReceived;
            Connection.ServiceClosed += Connection_ServiceClosed;

            AppServiceConnectionStatus status = await Connection.OpenAsync();

        }

        private static void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {

        }

        private static async void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            ValueSet response = new ValueSet();

            foreach (var key in args.Request.Message.Keys)
            {
                switch (key)
                {
                    case "TestConnection":
                        {
                            response.Add("TestConnection", " Connection is OK");
                            break;
                        }
                }
            }
            await args.Request.SendResponseAsync(response);
        }
    }
}