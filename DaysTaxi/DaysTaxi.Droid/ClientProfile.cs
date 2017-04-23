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
    [Activity(Label = "ClientProfile")]
    public class ClientProfile : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.ClientProfile);
            TextView text = FindViewById<TextView>(Resource.Id.cprofText);
            text.Text = LoadScreen.kinveyClient.ActiveUser.UserName;



            EditText fNameField = FindViewById<EditText>(Resource.Id.first_name);
            EditText lNameField = FindViewById<EditText>(Resource.Id.last_name);
            EditText emailField = FindViewById<EditText>(Resource.Id.email);
            EditText passField = FindViewById<EditText>(Resource.Id.pass);
            EditText phoneField = FindViewById<EditText>(Resource.Id.phone);
            EditText bDateField = FindViewById<EditText>(Resource.Id.date);

            fNameField.Text = (string)LoadScreen.kinveyClient.ActiveUser.Attributes["first_name"];
            lNameField.Text = (string)LoadScreen.kinveyClient.ActiveUser.Attributes["last_name"];
            emailField.Text = (string)LoadScreen.kinveyClient.ActiveUser.Attributes["email"];
            phoneField.Text = (string)LoadScreen.kinveyClient.ActiveUser.Attributes["phone"];
            bDateField.Text = (string)LoadScreen.kinveyClient.ActiveUser.Attributes["birth_date"];

            Button subBtn = FindViewById<Button>(Resource.Id.submitBtn);
            subBtn.Click += async delegate
              {
                  try
                  {
                      // If data is here
                      if (fNameField.Text != "" && lNameField.Text != "" && bDateField.Text != "" && phoneField.Text != "" && emailField.Text != "")
                      {

                          LoadScreen.kinveyClient.ActiveUser.Attributes["first_name"] = fNameField.Text;
                          LoadScreen.kinveyClient.ActiveUser.Attributes["last_name"] = lNameField.Text;
                          LoadScreen.kinveyClient.ActiveUser.Attributes["email"] = emailField.Text;
                          LoadScreen.kinveyClient.ActiveUser.Attributes["phone"] = phoneField.Text;
                          LoadScreen.kinveyClient.ActiveUser.Attributes["birth_date"] = bDateField.Text;


                          if (passField.Text.Length > 0)
                          {
                              LoadScreen.kinveyClient.ActiveUser.Attributes["password"] = passField.Text;
                          }

                          // Update a user with all of the data
                          User newUser = await LoadScreen.kinveyClient.ActiveUser.UpdateAsync();

                          StartActivity(typeof(ClientProfile));
                      }
                  }
                  catch(Exception ex)
                  {

                  }
                  
              };
        }
    }
}