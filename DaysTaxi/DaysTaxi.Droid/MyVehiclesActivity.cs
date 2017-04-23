using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using Kinvey;

namespace DaysTaxi.Droid
{
    [Activity(Label = "My Vehicles")]
    class MyVehiclesActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Client client=LoadScreen.BuildClient();

            // Create your application here

            SetContentView(Resource.Layout.MyVehicles);

            Button addCar = FindViewById<Button>(Resource.Id.addCarBtn);
            addCar.Click += delegate {
                StartActivity(typeof(AddCarActivity));
            };

            try
            {

                DataStore<Book> books = DataStore<Book>.Collection("books", DataStoreType.NETWORK);

                DataStore<Car> cars = DataStore<Car>.Collection("cars", DataStoreType.NETWORK);



                List<Car> carItems = await cars.FindAsync();


                ListView listView;
                listView = FindViewById<ListView>(Resource.Id.vehsListView); // get reference to the ListView in the layout
                listView.Adapter = new CarRowAdapter(this, carItems);      // populate the listview with data

            }
            catch(Exception ex)
            {
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
            
        }

        
    }
}