using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using EngagementApp.Adapters;
using System.Collections.Generic;
using AndroidX.RecyclerView.Widget;
using System.Threading.Tasks;
using Android.Content;
using Google.Android.Material.Navigation;
using AndroidX.DrawerLayout.Widget;
using AndroidX.Core.View;
using Java.IO;
using EngagementApp.DB;
using Android.Graphics.Drawables;
using AndroidX.ViewPager.Widget;
using TheArtOfDev.Edmodo.Cropper;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace EngagementApp
{
    [Activity(Label = "@string/app_name", Theme= "@style/AppTheme.NoActionBar")]
    public class SingleDisplayActivity : AppCompatActivity
    {
        ProgressDialog progress;
        Task startupWork1;
        List<GalleryviewDataSource> listItems;
        ViewPagerAdapter mViewPagerAdapter;
        string CatName;
        int Index = 0;
        public int CurrentIndex { get; set; }
        FrameLayout FrameLayoutContainer;
        public EngagementApp.Fragments.FloatingFragment FloatingFragment ;
        AndroidX.Fragment.App.FragmentTransaction trans;
        ViewPager mViewPager;

        public SingleDisplayActivity()
        {

        }
        public SingleDisplayActivity(int position)
        {
            this.CurrentIndex = position;

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_signledisplay);


            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

          


            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(true);


            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("جارى التحميل  يرجى الانتظار");
            progress.SetCancelable(false);
            progress.Show();

            int CatID = Intent.GetIntExtra("CatID", 0);
            CatName = Intent.GetStringExtra("CatName");
            Index = Intent.GetIntExtra("PhotoIndex", 0);
            listItems = new List<GalleryviewDataSource>();
            
             mViewPager = FindViewById<ViewPager>(Resource.Id.viewPagerMain);
            
            // Initializing the ViewPagerAdapter
             mViewPagerAdapter = new ViewPagerAdapter(this,listItems);
           
            startupWork1 = new Task(() => { SimulateStartup(CatID, mViewPagerAdapter, mViewPager); });

            startupWork1.Start();
            FrameLayoutContainer = FindViewById<FrameLayout>(Resource.Id.frameLayout1);

           

            FloatingActionButton button = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton1);
          
            button.Click += Button_Click;

         
          
           

        }

      

        private void Button_Click(object sender, EventArgs e)
        {

            if (!FloatingFragment.IsVisible)
            {
                

                var ntrans = SupportFragmentManager.BeginTransaction();
                
                ntrans.SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom);
                ntrans.Show(FloatingFragment);
                ntrans.Commit();
            }
            else
            {

            

                var ntrans = SupportFragmentManager.BeginTransaction();
                
                ntrans.SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom);
                ntrans.Hide(FloatingFragment);
                ntrans.Commit();
            }
               
           


        }

        public async void SimulateStartup(int CatID, ViewPagerAdapter mViewPagerAdapter, ViewPager recyclerView)
        {
            listItems = new List<GalleryviewDataSource>();
            listItems = LoadAllImagesByCatID(CatID);
            if (listItems.Count == 0)
            {

                listItems = new List<GalleryviewDataSource>();

                listItems.Add(new GalleryviewDataSource(null, GetDrawable(Resource.Drawable.ic_pictures)));
                listItems.Add(new GalleryviewDataSource(null, GetDrawable(Resource.Drawable.ic_pictures)));
                listItems.Add(new GalleryviewDataSource(null, GetDrawable(Resource.Drawable.ic_pictures)));

            }
            mViewPagerAdapter = new ViewPagerAdapter(this, listItems);
            RunOnUiThread(() =>
            {

               
                recyclerView.Adapter = mViewPagerAdapter;
                recyclerView.SetCurrentItem(Index, true);
                FloatingFragment = new Fragments.FloatingFragment(recyclerView, listItems);

                trans = SupportFragmentManager.BeginTransaction();
                trans.Add(Resource.Id.frameLayout1, FloatingFragment, "FloatingFragment");
                trans.Hide(FloatingFragment);
                trans.Commit();



                if (startupWork1.IsCompletedSuccessfully)
                {

                    progress.Hide();

                }


            });



        }

        public List<GalleryviewDataSource> LoadAllImagesByCatID(int CatID)
        {
            List<GalleryviewDataSource> mainGridviewDataSources = new List<GalleryviewDataSource>();
            SQLLiteDB sQLLiteDB = new SQLLiteDB();
            List<PhotoCategories> photoCategories = sQLLiteDB.GetAllCategories();
            string CatName = photoCategories.Find(x => x.CatID == CatID).CatName;
            Java.IO.File file = new Java.IO.File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), CatName);


            if (file.Exists())
            {
                File[] files = file.ListFiles();
                if (files.Length > 0)
                {


                    foreach (File f in files)
                    {
                        String absolutePath = f.AbsolutePath + Java.IO.File.Separator;

                        mainGridviewDataSources.Add(new GalleryviewDataSource(Android.Net.Uri.FromFile(f), Drawable.CreateFromPath(f.AbsolutePath)));




                    }
                }
            }

            return mainGridviewDataSources;
        }


        public override void OnBackPressed()
        {
          
            if (FloatingFragment.IsVisible)
            {


                var ntrans = SupportFragmentManager.BeginTransaction();

                ntrans.SetCustomAnimations(Resource.Animation.abc_slide_in_bottom, Resource.Animation.abc_slide_out_bottom);
                ntrans.Hide(FloatingFragment);
                ntrans.Commit();
            }
            else
            {
                base.OnBackPressed();
            }

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
             
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);


            if (requestCode == CropImage.CropImageActivityRequestCode)
            {
                CropImage.ActivityResult result = CropImage.GetActivityResult(data);
                if (resultCode == Result.Ok)
                {
                    Android.Net.Uri resultUri = result.Uri;

                    int index = mViewPager.CurrentItem;

                    listItems[index].PhotoPath = resultUri;

                    trans = SupportFragmentManager.BeginTransaction();
                    trans.Hide(FloatingFragment);
                    trans.Commit();

                    mViewPagerAdapter = new ViewPagerAdapter(this, listItems);
                    mViewPager.Adapter = mViewPagerAdapter;
                    mViewPager.SetCurrentItem(index, true);


                }
                else if (requestCode == CropImage.CropImageActivityResultErrorCode)
                {
                    Exception error = result.Error;
                    Toast.MakeText(this, "error", ToastLength.Long).Show();

                }
                else
                {
                    Toast.MakeText(this, "what", ToastLength.Long).Show();

                }
            }
        }
    }
}