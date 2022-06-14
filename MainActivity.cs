using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using AndroidX.RecyclerView.Widget;
using EngagementApp.Adapters;
using EngagementApp.DB;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using System.Linq;

namespace EngagementApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity , NavigationView.IOnNavigationItemSelectedListener
    {

        Button btnfarah;
        Button btnfatha;
        Button btnhoneymonth;
        Button btnkhtoba;
        Button btnborn;
        Button btnktbktab;

        ProgressDialog progress;
        Task startupWork7;
        Task startupWork2;
        Task startupWork3;
        Task startupWork4;
        Task startupWork5;
        Task startupWork6;


        List<MainGridviewDataSource> listItems;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
          

            SetSupportActionBar(toolbar);


            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("جارى التحميل  يرجى الانتظار");
            progress.SetCancelable(false);
            progress.Show();

            MainGridViewAdapter MainGridViewAdapter = new MainGridViewAdapter(this, listItems);


            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            LinearLayoutManager linearLayoutManager2 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            LinearLayoutManager linearLayoutManager3 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            LinearLayoutManager linearLayoutManager4 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            LinearLayoutManager linearLayoutManager5 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            LinearLayoutManager linearLayoutManager6 = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);

            RecyclerView maingridView = FindViewById<RecyclerView>(Resource.Id.gridViewmain);
            RecyclerView maingridView2 = FindViewById<RecyclerView>(Resource.Id.gridViewmain2);
            
            RecyclerView maingridView3 = FindViewById<RecyclerView>(Resource.Id.gridViewmain3);
            RecyclerView maingridView4 = FindViewById<RecyclerView>(Resource.Id.gridViewmain4);
            RecyclerView maingridView5 = FindViewById<RecyclerView>(Resource.Id.gridViewmain5);
            RecyclerView maingridView6 = FindViewById<RecyclerView>(Resource.Id.gridViewmain6);


            maingridView.SetLayoutManager(linearLayoutManager);
            maingridView2.SetLayoutManager(linearLayoutManager2);
            maingridView3.SetLayoutManager(linearLayoutManager3);
            maingridView4.SetLayoutManager(linearLayoutManager4);
            maingridView5.SetLayoutManager(linearLayoutManager5);
            maingridView6.SetLayoutManager(linearLayoutManager6);



            btnfatha = FindViewById<Button>(Resource.Id.btnfatha);
            btnfatha.Click += (s, e) =>
            {

                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 1);
                intent.PutExtra("CatName", btnfatha.Text);
                StartActivity(intent);



            };
             btnkhtoba = FindViewById<Button>(Resource.Id.btnkhtooba);
            btnkhtoba.Click += (s, e) =>
            {

                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 2);
                intent.PutExtra("CatName", btnkhtoba.Text);
                StartActivity(intent);



            };
             btnktbktab = FindViewById<Button>(Resource.Id.btnktbktab);
            btnktbktab.Click += (s, e) =>
            {

                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 3);
                intent.PutExtra("CatName", btnktbktab.Text);
                StartActivity(intent);



            };


             btnfarah = FindViewById<Button>(Resource.Id.btnfarah);
            btnfarah.Click += (s, e) =>
            {

                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 4);
                intent.PutExtra("CatName", btnfarah.Text);
                StartActivity(intent);



            };

             btnhoneymonth = FindViewById<Button>(Resource.Id.btnhoneymonth);
            btnhoneymonth.Click += (s, e) =>
            {

                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 5);
                intent.PutExtra("CatName", btnhoneymonth.Text);
                StartActivity(intent);



            };
             btnborn = FindViewById<Button>(Resource.Id.btnborn);
            btnborn.Click += (s, e) =>
            {

                Intent intent = new Intent(this, typeof(DisplayActivity));
                intent.PutExtra("CatID", 6);
                intent.PutExtra("CatName", btnborn.Text);
                StartActivity(intent);



            };














            startupWork2 = new Task(() => { SimulateStartup(2, MainGridViewAdapter, maingridView2); });

                startupWork2.Start();









           startupWork3 = new Task(() => { SimulateStartup(3, MainGridViewAdapter, maingridView3); });

          
                startupWork3.Start();

         





          startupWork4 = new Task(() => { SimulateStartup(4, MainGridViewAdapter, maingridView4); });


         
                startupWork4.Start();








        startupWork5 = new Task(() => { SimulateStartup(5, MainGridViewAdapter, maingridView5); });
            startupWork5.Start();




          startupWork6 = new Task(() => { SimulateStartup(6, MainGridViewAdapter, maingridView6); });
            startupWork6.Start();






            startupWork7 = new Task(() => { SimulateStartup(1, MainGridViewAdapter, maingridView); });

            startupWork7.Start();







            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton1);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.app_name, Resource.String.app_name);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            navigationView.Menu.GetItem(0).SetChecked(true);
            if (navigationView.Menu.GetItem(1).IsChecked)
            {
                navigationView.Menu.GetItem(1).Icon.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcAtop);

            }
            else
            {
                
                navigationView.Menu.GetItem(1).Icon.SetColorFilter(Color.ParseColor("#34568B"), PorterDuff.Mode.SrcAtop);
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

        public async void SimulateStartup(int CatID , MainGridViewAdapter mainGridViewAdapter , RecyclerView recyclerView)
        {
            listItems = new List<MainGridviewDataSource>();
            
            listItems = LoadAllImagesByCatID(CatID);
            if (listItems.Count == 0)
            {

                listItems = new List<MainGridviewDataSource>();

                listItems.Add(new MainGridviewDataSource(null, GetDrawable(Resource.Drawable.ic_pictures)));
                listItems.Add(new MainGridviewDataSource(null, GetDrawable(Resource.Drawable.ic_pictures)));
               
            }
            mainGridViewAdapter = new MainGridViewAdapter(this, listItems);
            RunOnUiThread(() =>
            {
              

                recyclerView.SetAdapter(mainGridViewAdapter);

              

               
              
              
                if (startupWork7.IsCompletedSuccessfully &&
            startupWork2.IsCompletedSuccessfully &&
            startupWork3.IsCompletedSuccessfully &&
            startupWork4.IsCompletedSuccessfully &&
            startupWork5.IsCompletedSuccessfully &&
            startupWork6.IsCompletedSuccessfully)
                {

                    progress.Hide();

                }


            });
          
            

        }
 
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View) sender;
            //Snackbar.Make(view, "أنت على وشك إضافة صور للاستوديو الخاص بك هل تريد المتابعة ؟", Snackbar.LengthIndefinite)

            //    .SetAction(" متابعة", (v)=> {

            //        Toast.MakeText(this, "جارى التحميل يرجى الانتظار.....", ToastLength.Long).Show();

            //        Task task = new Task(() => { DelayThreadBeforeOpenSelectActivity(); });

            //        task.Start();

            //    })
            //    .Show();

            Intent intent = new Intent(this, typeof(SelectActivity));
            StartActivity(intent);

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
        //protected async void DelayThreadBeforeOpenSelectActivity()
        //{
        //    await Task.Delay(2000);
           
        //}

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


        public  List<MainGridviewDataSource> LoadAllImagesByCatID(int CatID)
        {
            List<MainGridviewDataSource> mainGridviewDataSources = new List<MainGridviewDataSource>();
            SQLLiteDB sQLLiteDB = new SQLLiteDB();
            List<PhotoCategories> photoCategories = sQLLiteDB.GetAllCategories();
            string CatName = photoCategories.Find(x => x.CatID == CatID).CatName;
            Java.IO.File file = new Java.IO.File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), CatName);

       
                        if (file.Exists())
                        {
                            File[] files = file.ListFiles();
                if (files.Length>0)
                {

                
                            foreach (File f in files.Reverse())
                            {
                        String absolutePath = f.AbsolutePath+ Java.IO.File.Separator;
                                
                                mainGridviewDataSources.Add(new MainGridviewDataSource(Android.Net.Uri.FromFile(f), Drawable.CreateFromPath(f.AbsolutePath)));


                        if (mainGridviewDataSources.Count>=20)
                        {
                            break;
                        }
                          
                           

                            
                          
                            }

                  
                }
            }

                        return mainGridviewDataSources;
        }

             
        
    }
}
