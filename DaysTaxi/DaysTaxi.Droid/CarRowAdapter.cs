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
    
    class CarRowAdapter: BaseAdapter<Car>
    {
        List<Car> items;

        Activity context;



        public CarRowAdapter(Activity context, List<Car> items)
       : base()
        {
            this.context = context;
            this.items = items;
        }



        public override long GetItemId(int position)
        {
            return position;
        }



        public override Car this[int position]
        {
            get { return items[position]; }
        }



        public override int Count
        {
            get { return items.Count; }
        }



        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CarRowLayout, null);
            view.FindViewById<TextView>(Resource.Id.nameTxt).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.classTxt).Text = item.Class.ToString();



            Android.Net.Uri.Builder ss = new Android.Net.Uri.Builder();
            ss.Path(item.PhotoID);


            view.FindViewById<ImageView>(Resource.Id.photoImg).SetImageURI(ss.Build());
            return view;
            
        }

    }
    
}