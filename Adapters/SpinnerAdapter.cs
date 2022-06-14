using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EngagementApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngagementApp.Adapters
{
    class SpinnerAdapter : BaseAdapter
    {

        Context context;
        List<PhotoCategories> listItems = new List<PhotoCategories>();

        public SpinnerAdapter(Context context, List<PhotoCategories> listItems)
        {
            this.context = context;
            PhotoCategories photoCategories1 = new PhotoCategories();
            photoCategories1.CatID = 0;
            photoCategories1.CatName = "اختر فئة للصورة";
            this.listItems.Add(photoCategories1);
            this.listItems.AddRange(listItems);
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            SpinnerAdapterViewHolder holder = null;
            var item = listItems[position];

            if (view != null)
                holder = view.Tag as SpinnerAdapterViewHolder;

            if (holder == null)
            {
                holder = new SpinnerAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.spinner_item, parent, false);
                holder.CatName = view.FindViewById<TextView>(Resource.Id.textView1);
                view.Tag = holder;
            }


            //fill in your items
            holder.CatName.Text = item.CatName;
            switch (item.CatID)
            {
                case 1:holder.CatName.SetCompoundDrawablesWithIntrinsicBounds(context.GetDrawable(Resource.Drawable.ic_fatha), null, null, null);break;
                case 2:holder.CatName.SetCompoundDrawablesWithIntrinsicBounds(context.GetDrawable(Resource.Drawable.ic_engagement), null, null, null); break;
                case 3: holder.CatName.SetCompoundDrawablesWithIntrinsicBounds(context.GetDrawable(Resource.Drawable.ic_ktbktab), null, null, null); break;
                case 4: holder.CatName.SetCompoundDrawablesWithIntrinsicBounds(context.GetDrawable(Resource.Drawable.ic_farah), null, null, null); break;
                case 5: holder.CatName.SetCompoundDrawablesWithIntrinsicBounds(context.GetDrawable(Resource.Drawable.ic_honeymonth), null, null, null); break;
                case 6: holder.CatName.SetCompoundDrawablesWithIntrinsicBounds(context.GetDrawable(Resource.Drawable.ic_pregnancy), null, null, null); break;


            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return listItems.Count;
            }
        }

    }

    class SpinnerAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView CatName { get; set; }
    }
}