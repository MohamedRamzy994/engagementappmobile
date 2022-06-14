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
using Android.Graphics;

namespace EngagementApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class DisplayActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        ProgressDialog progress;
        Task startupWork1;
        List<GalleryviewDataSource> listItems;
        GalleryViewAdapter GalleryViewAdapter;
        TextView btncatdisplay;
        string CatName;
        int CatID;

        RelativeLayout RelativeLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_display);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);


            SetSupportActionBar(toolbar);


            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("جارى التحميل  يرجى الانتظار");
            progress.SetCancelable(false);
            progress.Show();
           

            CatID=  Intent.GetIntExtra("CatID",0);
            CatName = Intent.GetStringExtra("CatName");


           btncatdisplay = FindViewById<TextView>(Resource.Id.btncatdisplay);
           

            listItems = new List<GalleryviewDataSource>();
            GridView displaygridview = FindViewById<GridView>(Resource.Id.displaygridview);
            GalleryViewAdapter = new GalleryViewAdapter(this, listItems);
            startupWork1 = new Task(() => { SimulateStartup(CatID, GalleryViewAdapter, displaygridview); });

            startupWork1.Start();








            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton1);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.app_name, Resource.String.app_name);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
           

            switch (CatID) {

                case 1: navigationView.Menu.GetItem(1).SetChecked(true);
                    btncatdisplay.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.ic_fatha), null, GetDrawable(Resource.Drawable.ic_fatha), null); break;
                case 2: navigationView.Menu.GetItem(2).SetChecked(true); 
                    btncatdisplay.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.ic_engagement), null, GetDrawable(Resource.Drawable.ic_engagement), null); break;
                case 3: navigationView.Menu.GetItem(3).SetChecked(true); 
                    btncatdisplay.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.ic_ktbktab), null, GetDrawable(Resource.Drawable.ic_ktbktab), null); break;
                case 4: navigationView.Menu.GetItem(4).SetChecked(true);
                    btncatdisplay.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.ic_farah), null, GetDrawable(Resource.Drawable.ic_farah), null); break;
                case 5: navigationView.Menu.GetItem(5).SetChecked(true); 
                    btncatdisplay.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.ic_honeymonth), null, GetDrawable(Resource.Drawable.ic_honeymonth), null); break;
                case 6: navigationView.Menu.GetItem(6).SetChecked(true);
                    btncatdisplay.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.ic_pregnancy), null, GetDrawable(Resource.Drawable.ic_pregnancy), null); break;
                



            }

            if (navigationView.Menu.GetItem(1).IsChecked)
            {

                navigationView.Menu.GetItem(1).Icon.SetColorFilter(Color.ParseColor("#ffffff"),PorterDuff.Mode.Multiply);
                
            }
            else
            {
                navigationView.Menu.GetItem(1).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.Multiply);
            }

            if (navigationView.Menu.GetItem(2).IsChecked)
            {
                navigationView.Menu.GetItem(2).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                navigationView.Menu.GetItem(2).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
            }
            if (navigationView.Menu.GetItem(3).IsChecked)
            {
                navigationView.Menu.GetItem(3).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                navigationView.Menu.GetItem(3).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
            }
             if (navigationView.Menu.GetItem(4).IsChecked)
            {
                navigationView.Menu.GetItem(4).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                navigationView.Menu.GetItem(4).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
            }
             if (navigationView.Menu.GetItem(5).IsChecked)
            {
                navigationView.Menu.GetItem(5).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                navigationView.Menu.GetItem(5).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
            }
             if (navigationView.Menu.GetItem(6).IsChecked)
            {
                navigationView.Menu.GetItem(6).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                navigationView.Menu.GetItem(6).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
            }
             if (navigationView.Menu.GetItem(0).IsChecked)
            {
                navigationView.Menu.GetItem(0).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                navigationView.Menu.GetItem(0).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
            }



        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View)sender;
            //Snackbar.Make(view, "أنت على وشك إضافة صور للاستوديو الخاص بك هل تريد المتابعة ؟", Snackbar.LengthIndefinite)

            //    .SetAction(" متابعة", (v) => {

            //        Toast.MakeText(this, "جارى التحميل يرجى الانتظار.....", ToastLength.Long).Show();

            //        Task task = new Task(() => { DelayThreadBeforeOpenSelectActivity(); });

            //        task.Start();

            //    })
            //    .Show();

            Intent intent = new Intent(this, typeof(SelectActivity));
            StartActivity(intent);
        }
        //protected async void DelayThreadBeforeOpenSelectActivity()
        //{
        //    //await Task.Delay(2000);
            
        //}

        public async void SimulateStartup(int CatID, GalleryViewAdapter mainGridViewAdapter, GridView recyclerView)
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
            mainGridViewAdapter = new GalleryViewAdapter(this ,listItems);
            RunOnUiThread(() =>
            {

                btncatdisplay.Text = CatName + " "+"(" + listItems.Count + " صورة" + ")";
                recyclerView.Adapter=mainGridViewAdapter;
                recyclerView.ItemClick += (s, e) =>
                {
                    Intent intent = new Intent(this, typeof(SingleDisplayActivity));
                    intent.PutExtra("CatID", CatID);
                    intent.PutExtra("CatName", CatName);
                    intent.PutExtra("PhotoIndex", e.Position);
                    StartActivity(intent);
                };

                if (startupWork1.IsCompletedSuccessfully)
                {

                    progress.Hide();

                }


            });



        }

        public override void OnBackPressed()
        {

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }
       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            int id = menuItem.ItemId;


            if (id == Resource.Id.home)
            {
                // Handle the camera action
                Intent hIntent = new Intent(this, typeof(MainActivity));
                StartActivity(hIntent);
            }
            else if (id == Resource.Id.fatha)
            {
                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 1);
                intent.PutExtra("CatName", menuItem.TitleFormatted);
                StartActivity(intent);


            }
            else if (id == Resource.Id.khtooba)
            {
                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 2);
                intent.PutExtra("CatName", menuItem.TitleFormatted);
                StartActivity(intent);


            }
            else if (id == Resource.Id.ktbtab)
            {
                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 3);
                intent.PutExtra("CatName", menuItem.TitleFormatted);
                StartActivity(intent);


            }
            else if (id == Resource.Id.farah)
            {
                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 4);
                intent.PutExtra("CatName", menuItem.TitleFormatted);
                StartActivity(intent);
            }
            else if (id == Resource.Id.honeymonth)
            {
                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 5);
                intent.PutExtra("CatName", menuItem.TitleFormatted);
                StartActivity(intent);

            }
            else if (id == Resource.Id.born)
            {
                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 6);
                intent.PutExtra("CatName", menuItem.TitleFormatted);
                StartActivity(intent);
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
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
                        String absolutePath = f.AbsolutePath+Java.IO.File.Separator;
                  
                            mainGridviewDataSources.Add(new GalleryviewDataSource(Android.Net.Uri.FromFile(f), Drawable.CreateFromPath(f.AbsolutePath)));




                   }
                }
            }

            return mainGridviewDataSources;
        }



    }
}
