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
using Android.Text.Method;

namespace DaysTaxi.Droid
{
    [Activity(Label = "HomePageActivity")]
    public class HomePageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.HomePage);

            TextView confLink = FindViewById<TextView>(Resource.Id.confLink);
            Button submitBtn = FindViewById<Button>(Resource.Id.submitBtn);
            TextView resendBtn = FindViewById<TextView>(Resource.Id.resendBtn);

            submitBtn.Click += delegate
            {
                var uri = Android.Net.Uri.Parse(confLink.Text);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

                
              //  StartActivity(typeof(LoadScreen));
            };

            resendBtn.MovementMethod = LinkMovementMethod.Instance;

            resendBtn.Click += async delegate
              {
                  User myUser=await LoadScreen.kinveyClient.ActiveUser.EmailVerificationAsync(LoadScreen.kinveyClient.ActiveUser.Id);
              };

        }
    }
}