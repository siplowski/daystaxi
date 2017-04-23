using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Kinvey;
using SQLite.Net.Platform.XamarinAndroid;

namespace DaysTaxi.Droid
{
    [Activity(Label = "login", MainLauncher = true)]
    public class LoadScreen : Activity
    {
        
        public static Client kinveyClient;

        public LoadScreen()
        {
            kinveyClient = BuildClient();
            
        }

        public static Client BuildClient()
        {
            string app_key = "kid_HkY2vPKte";
            string app_secret = "2301f74525aa42efa7b7e89ed67705fa";

            Client.Builder cb = new Client.Builder(app_key, app_secret)
              .setLogger(delegate (string msg) { Console.WriteLine(msg); })
            .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            // .setOfflinePlatform(new SQLitePlatformAndroid());
            
            return cb.Build(); 
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoadScreen);

            /*
            try
            {
                PingResponse response = await kinveyClient.PingAsync();
            }
            catch (Exception ex)
            {
                // an error has occured
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle(ex.Message);

                alert.SetPositiveButton("Good", (senderAlert, args) => {
                });

                alert.SetNegativeButton("Bad", (senderAlert, args) => {
                });

                //run the alert in UI thread to display in the screen
                RunOnUiThread(() => {
                    alert.Show();
                });
            }
            */

            Button loginButton = FindViewById<Button>(Resource.Id.loginBtn);

            loginButton.Click += async delegate
            {
                string username = FindViewById<EditText>(Resource.Id.email).Text;
                string password = FindViewById<EditText>(Resource.Id.pass).Text;

               
                try
                {
                    User myUser = await User.LoginAsync(username, password);

                    if (myUser.Metadata.EmailVerification.Status == "confirmed")
                    {
                        if(myUser.Attributes["role"].ToString()=="client")
                            StartActivity(typeof(ClientProfile));

                        if (myUser.Attributes["role"].ToString() == "driver")
                            StartActivity(typeof(DriverProfileActivity));
                    } 
                    else
                    {
                        StartActivity(typeof(LoadScreen));
                    }
                }
                catch(KinveyException ex)
                {
                    
                }

            };



            Button clientSignupButton = FindViewById<Button>(Resource.Id.clientSignupBtn);
            clientSignupButton.Click += delegate
             {
                 StartActivity(typeof(ClientSignupActivity));
             };



            Button driverSignupButton = FindViewById<Button>(Resource.Id.driverSignupBtn);
            driverSignupButton.Click += delegate
            {
                StartActivity(typeof(DriverSignupActivity));
            };
        }


    }
}