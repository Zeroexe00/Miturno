using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase;
using Xamarin.Forms;
using Android.Gms.Common;
using Android.Content;
using Android.Gms.Auth.Api.SignIn;
using Firebase.Iid;
using Firebase.Messaging;

namespace MasterDetail.Droid
{
    [Activity(Label = "MiTurnoApp", Icon = "@drawable/logoapp", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            CheckForGoogleServices();
        }

        // Check if the Google Play Services is available to recieve Push Notification from Firebase
        public bool CheckForGoogleServices()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(this, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(this, "This device does not support Google Play Services", ToastLength.Long);
                }
                return false;
            }
            return true;
        }


        [Service]
        [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
        public class MyFirebaseIIDService : FirebaseInstanceIdService
        {
            public override void OnTokenRefresh()
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                Console.WriteLine($"Token received: {refreshedToken}");
                SendRegistrationToServerAsync(refreshedToken);
            }

            void SendRegistrationToServerAsync(string token)
            {
                //holi
            }


        }
        // This service is used if app is in the foreground and a message is received.
        [Service]
        [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
        public class MyFirebaseMessagingService : FirebaseMessagingService
        {
            public override void OnMessageReceived(RemoteMessage message)
            {
                base.OnMessageReceived(message);

                Console.WriteLine("Received: " + message);

                // Android supports different message payloads. To use the code below it must be something like this (you can paste this into Azure test send window):
                // {
                //   "notification" : {
                //      "body" : "The body",
                //                 "title" : "The title",
                //                 "icon" : "myicon
                //   }
                // }
                try
                {
                    var msg = message.GetNotification().Body;
                    MessagingCenter.Send<object, string>(this, MasterDetail.App.NotificationReceivedKey, msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error extracting message: " + ex);
                }
            }
        }
    }
}