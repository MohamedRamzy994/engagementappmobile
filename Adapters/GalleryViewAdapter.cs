using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngagementApp.Adapters
{
    public class GalleryViewAdapter : BaseAdapter
    {

        Context context;
        List<GalleryviewDataSource> Items;
        public event EventHandler<int> ItemClick;



        public GalleryViewAdapter(Context context,List<GalleryviewDataSource> Items):base()
        {
            this.context = context;
            this.Items = Items;
        }

        public override int Count => Items.Count;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            var item = Items[position];
          

                //var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            
                view = LayoutInflater.From(context).Inflate(Resource.Layout.display_gridview_ticket, parent, false);
               ImageView imageview  = view.FindViewById<ImageView>(Resource.Id.icon);

               Glide.With(parent).Load(item.PhotoPath).Into(imageview);


            //imageview.SetImageDrawable(item.PhotoDrawable);
              

            return view;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
    }

 
   public class GalleryviewDataSource
    {

        public string PhotoId { get; set; }

        public Android.Net.Uri PhotoPath { get; set; }
        public Drawable PhotoDrawable { get; set; }



        public GalleryviewDataSource(Android.Net.Uri PhotoPath, Drawable PhotoDrawable)
        {
            this.PhotoId = Guid.NewGuid().ToString();
            this.PhotoPath = PhotoPath;
            this.PhotoDrawable = PhotoDrawable;
        }

    }
}