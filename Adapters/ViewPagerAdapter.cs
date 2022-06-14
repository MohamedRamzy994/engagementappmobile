using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using Bumptech.Glide;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngagementApp.Adapters
{
     public class ViewPagerAdapter : PagerAdapter
    {
        EngagementApp.Fragments.FloatingFragment FloatingFragment;

        Context context;
        List<GalleryviewDataSource> Items;
        private LayoutInflater layoutInflater;
       

        public ViewPagerAdapter(Context context, List<GalleryviewDataSource> Items)
        {
            this.context = context;
            this.Items = Items;
           
        }

        public override int Count =>  Items.Count;

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view == @object;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            

            var item = Items[position];

          
         

            layoutInflater =(LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);

            View view = layoutInflater.Inflate(Resource.Layout.activity_signledisplayitem,null);
            ImageView imageview = view.FindViewById<ImageView>(Resource.Id.imageViewMain);

            imageview.SetScaleType(ImageView.ScaleType.FitCenter);

            Glide.With(context).Load(item.PhotoPath).Into(imageview);

            ViewPager vp =(ViewPager) container;
            vp.AddView(view);

          

            return view;


        }

        public override void DestroyItem(View container, int position, Java.Lang.Object @object)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(@object as View);
        }

       




    }

}