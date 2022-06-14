using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;
using Bumptech.Glide;

namespace EngagementApp.Adapters
{
    public class MainGridViewAdapter : RecyclerView.Adapter
    {

        Context context;
        List<MainGridviewDataSource> Items;

        public event EventHandler<int> ItemClick;



        public MainGridViewAdapter(Context context,List<MainGridviewDataSource> Items):base()
        {
            this.context = context;
            this.Items = Items;
        }



        public override long GetItemId(int position)
        {
            return long.Parse(Items[position].PhotoId);
        }



        public override int ItemCount => this.Items.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MainGridViewAdapterAdapterViewHolder vh = holder as MainGridViewAdapterAdapterViewHolder;
            var item = Items[position];
            
            
          

            if (item.PhotoPath== null)
            {

                vh.PhotoName.SetImageResource(Resource.Drawable.ic_launcher);

            }
            else
            {
                Glide.With(this.context).Load(item.PhotoPath).Into(vh.PhotoName);
            }

        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                       Inflate(Resource.Layout.ticket_gridview, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
  
            MainGridViewAdapterAdapterViewHolder vh =
                new MainGridViewAdapterAdapterViewHolder(itemView, OnClick);
            return vh;
        }
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);

           
        }
        internal class MainGridViewAdapterAdapterViewHolder : RecyclerView.ViewHolder
        {

            public ImageView PhotoName { get; private set; }





            // Get references to the views defined in the CardView layout.
            public MainGridViewAdapterAdapterViewHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
                // Locate and cache view references:

                PhotoName = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
   


                // Detect user clicks on the item view and report which item
                // was clicked (by layout position) to the listener:
                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

    }
    }


public class MainGridviewDataSource
{

    public string PhotoId { get; set; }

    public Android.Net.Uri PhotoPath { get; set; }
    public Drawable PhotoDrawable { get; set; }



    public MainGridviewDataSource(Android.Net.Uri PhotoPath,Drawable PhotoDrawable)
    {
        this.PhotoId = Guid.NewGuid().ToString();
        this.PhotoPath = PhotoPath;
        this.PhotoDrawable = PhotoDrawable;
    }

}