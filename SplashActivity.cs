using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using EngagementApp.DB;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngagementApp
{
    [Activity(Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
          


        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();

            SetContentView(Resource.Layout.activity_splashscreen);
            File rootFile = new File(Application.Context.GetExternalFilesDir(null), "ستوديو_حياتى");
            if (!rootFile.Exists())
            {
                rootFile.Mkdirs();

              

                DB.SQLLiteDB sQLLiteDB = new DB.SQLLiteDB();
                if (sQLLiteDB.GetAllCategories().Count==0)
                {
                    sQLLiteDB.InsertCategories();
                }
                
                List<PhotoCategories> photoCategories = sQLLiteDB.GetAllCategories();

                File fathaFile = new File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories[0].CatName );
                fathaFile.Mkdirs();

                File khtoobaFile = new File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories[1].CatName);
                khtoobaFile.Mkdirs();
                File ktbktabFile = new File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories[2].CatName);
                ktbktabFile.Mkdirs();
                File farahFile = new File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories[3].CatName);
                farahFile.Mkdirs();
                File honeyFile = new File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories[4].CatName);
                honeyFile.Mkdirs();
                File bornFile = new File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories[5].CatName);
                bornFile.Mkdirs();


            }
          

            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
           
            await Task.Delay(3000); // Simulate a bit of startup work.
          
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}