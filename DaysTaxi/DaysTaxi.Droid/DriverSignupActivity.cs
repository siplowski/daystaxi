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
using Newtonsoft.Json.Linq;

namespace DaysTaxi.Droid
{
    [Activity(Label = "DriverSignupActivity")]
    public class DriverSignupActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            // Set a view from ClientSignUp android layout
            SetContentView(Resource.Layout.ClientSignup);

            // Define a registration button
            Button regBtn = FindViewById<Button>(Resource.Id.regBtn);

            // Set a click event on registration button
            regBtn.Click += async delegate
            {
                // Get a data from the registration form fields
                string username = FindViewById<EditText>(Resource.Id.email).Text;
                string password = FindViewById<EditText>(Resource.Id.pass).Text;
                string phone = FindViewById<EditText>(Resource.Id.phone).Text;
                string first_name = FindViewById<EditText>(Resource.Id.first_name).Text;
                string last_name = FindViewById<EditText>(Resource.Id.last_name).Text;
                string birth_date = FindViewById<EditText>(Resource.Id.birth_date).Text;


                Dictionary<string, JToken> attributes = new Dictionary<string, JToken>();

                if (username != "")
                {
                    attributes.Add("email", username);
                }


                if (phone != "")
                {
                    attributes.Add("phone", phone);
                }


                if (first_name != "")
                {
                    attributes.Add("first_name", first_name);
                }


                if (last_name != "")
                {
                    attributes.Add("last_name", last_name);
                }


                if (birth_date != "")
                {
                    attributes.Add("birth_date", birth_date);
                }


                // If data is here
                if (username != "" && password != "" && first_name != "" && last_name != "" && birth_date != "" && phone != "")
                {
                    attributes.Add("role", "driver");

                    // Sign up a user with all of the data
                    User newUser = await User.SignupAsync(username, password, attributes);
                }

                // Go to a HomePage
                StartActivity(typeof(HomePageActivity));
            };
        }
        }
}