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
using System.IO;
using Kinvey;

namespace DaysTaxi.Droid
{
    [Activity(Label = "Add Car")]
    public class AddCarActivity : Activity
    {

        Android.Net.Uri imageUri;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.AddVehicleLayout);


            // получаем элементы формы для ввода данных

            Spinner classSpinner = FindViewById<Spinner>(Resource.Id.classSpinner);

            EditText carNameField = FindViewById<EditText>(Resource.Id.carNameTxt);

            ImageView photoView = FindViewById<ImageView>(Resource.Id.selectCarPhotoView);


            try
            {

                DataStore<CarClass> classesStore = DataStore<CarClass>.Collection("carclasses", DataStoreType.SYNC);
                List<CarClass> classes = await classesStore.FindAsync();


                ArrayAdapter myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, classes);
                classSpinner.Adapter = myAdapter;

            }
            catch (Exception ex)
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



            Button selectImgBtn = FindViewById<Button>(Resource.Id.selImgBtn);
            selectImgBtn.Click += delegate
            {
                var imageIntent = new Intent();
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(imageIntent, "select an image"), 0);
            };



            Button submitBtn = FindViewById<Button>(Resource.Id.submitBtn);
            submitBtn.Click += async delegate {

                //upload a photo file
                FileMetaData fileMetaData = new FileMetaData();
                fileMetaData.fileName = carNameField.Text + "Photo";
                fileMetaData.mimetype = "image/jpeg";

                bool publicAccess = true;
                fileMetaData._public = publicAccess;
                byte[] content = System.IO.File.ReadAllBytes(getPathToImage(imageUri));
                int contentSize = (content.Length) * sizeof(byte);
                fileMetaData.size = contentSize;
                MemoryStream streamContent = new MemoryStream(content);
                FileMetaData fmd = await LoadScreen.kinveyClient.File().uploadAsync(fileMetaData, streamContent);
                //end of the file uploading

                DataStore<Car> carStore = DataStore<Car>.Collection("cars", DataStoreType.CACHE);

                Car newCar = new Car();
                newCar.Name = carNameField.Text;
                newCar.Class = (int)classSpinner.SelectedItem.Class.GetField("_id");
                newCar.PhotoID = fmd.id;

                await carStore.SaveAsync(newCar);

            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                var imageView =
                    FindViewById<ImageView>(Resource.Id.selectCarPhotoView);
                imageView.SetImageURI(data.Data);
                imageUri = data.Data;

                
            }
        }

        private String getPathToImage(Android.Net.Uri _uri)

        {

            // This function stores, and returns, the path to the image from the device. Not sure if needed.

            String path = null;

            String[] projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };

            using (Android.Database.ICursor cursor = this.ManagedQuery(_uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }

            return path;
        }
    }
}