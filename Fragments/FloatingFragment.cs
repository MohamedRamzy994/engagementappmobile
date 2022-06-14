using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;
using System.Text;
using EngagementApp.Adapters;
using Java.IO;
using Android.Provider;
using TheArtOfDev.Edmodo.Cropper;
using AndroidX.ViewPager.Widget;
using Android.Views.Animations;

namespace EngagementApp.Fragments
{
    public class FloatingFragment : AndroidX.Fragment.App.Fragment
    {
        const int PIC_CROP = 1;
        int count=0;
        List<GalleryviewDataSource> listItems;
        int position;
        ViewPager ViewPager;

        Context context;
        public FloatingFragment(ViewPager viewPager , List<GalleryviewDataSource> listItems)
        {

            this.ViewPager = viewPager;
            this.listItems = listItems;
           
          
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment


            var view = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.fragment_floatingsingle, container, false);

            Button btncrop = view.FindViewById<Button>(Resource.Id.btncrop);
            btncrop.Click += Btncrop_Click;
            Button btnrotate = view.FindViewById<Button>(Resource.Id.btnrotate);
            btnrotate.Click += Btnrotate_Click ;


            return view;
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void Btnrotate_Click(object sender, EventArgs e)
        {

            if (count==0)
            {
                var rotateAnimation = AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.RotateAnimation90);
                var img = this.Activity.FindViewById<ImageView>(Resource.Id.imageViewMain);
                img.StartAnimation(rotateAnimation);
            }
            else if (count == 1)
            {
                var rotateAnimation = AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.RotateAnimation180);
                var img = this.Activity.FindViewById<ImageView>(Resource.Id.imageViewMain);
                img.StartAnimation(rotateAnimation);
            }
            else if (count == 2)
            {
                var rotateAnimation = AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.RotateAnimation270);
                var img = this.Activity.FindViewById<ImageView>(Resource.Id.imageViewMain);
                img.StartAnimation(rotateAnimation);
            }
            else if (count == 3)
            {
                var rotateAnimation = AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.RotateAnimation360);
                var img = this.Activity.FindViewById<ImageView>(Resource.Id.imageViewMain);
                img.StartAnimation(rotateAnimation);
            }
            else
            {
                count = 0;
            }



            var trans = this.Activity.SupportFragmentManager.BeginTransaction();
            trans.Hide(this);
            trans.Commit();
            count++;
        }

        private void Btncrop_Click(object sender, EventArgs e)
        {

         position=   ViewPager.CurrentItem;

            CropImage.Activity(this.listItems[position].PhotoPath)
  .SetGuidelines(CropImageView.Guidelines.On)
  
  .Start(this.Activity);

         

        }
    }
}