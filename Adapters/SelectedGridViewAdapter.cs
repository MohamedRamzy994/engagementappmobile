using Android.App;
using Android.Content;
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
    public class SelectedGridViewAdapter : RecyclerView.Adapter
    {

        Context context;
        List<SelectedGridviewDataSource> Items;

        public event EventHandler<int> ItemClick;



        public SelectedGridViewAdapter(Context context,List<SelectedGridviewDataSource> Items):base()
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
            SelectedGridViewAdapterAdapterViewHolder vh = holder as SelectedGridViewAdapterAdapterViewHolder;
            var item = Items[position];
            //vh.PhotoName.SetImageURI(item.PhotoPath);

            Glide.With(this.context).Load(item.PhotoPath).Into(vh.PhotoName);




        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                       Inflate(Resource.Layout.ticket_gridview, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            SelectedGridViewAdapterAdapterViewHolder vh =
                new SelectedGridViewAdapterAdapterViewHolder(itemView, OnClick);
            return vh;
        }
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
        internal class SelectedGridViewAdapterAdapterViewHolder : RecyclerView.ViewHolder
        {

            public ImageView PhotoName { get; private set; }
      



            // Get references to the views defined in the CardView layout.
            public SelectedGridViewAdapterAdapterViewHolder(View itemView, Action<int> listener)
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


public class SelectedGridviewDataSource
{

    public string PhotoId { get; set; }

    public Android.Net.Uri PhotoPath { get; set; }


    public SelectedGridviewDataSource(Android.Net.Uri PhotoPath)
    {
        this.PhotoId = Guid.NewGuid().ToString();
        this.PhotoPath = PhotoPath;
    }

}