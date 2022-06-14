using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using EngagementApp.Adapters;
using System;
using System.Collections.Generic;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Android.Graphics;
using System.IO;
using Android.Provider;
using EngagementApp.DB;
using System.Threading;
using Android.Graphics.Drawables;
using System.Threading.Tasks;
using Google.Android.Material.Snackbar;

namespace EngagementApp
{
    [Activity(Label = "@string/app_name",Theme = "@style/AppTheme.NoActionBar")]
    public class SelectActivity : AppCompatActivity
    {
        MainGridViewAdapter MainGridViewAdapter;
        SelectedGridViewAdapter SelectedGridViewAdapter;

        List<MainGridviewDataSource> listItems;
        List<SelectedGridviewDataSource> selectedListItems;
     
        ProgressDialog progress;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_select);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);


            

            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(true);



                selectedListItems = new List<SelectedGridviewDataSource>();
                listItems = new List<MainGridviewDataSource>();

                listItems.Add(new MainGridviewDataSource(null, GetDrawable(Resource.Drawable.ic_launcher)));
                listItems.Add(new MainGridviewDataSource(null, GetDrawable(Resource.Drawable.ic_launcher)));
              
          

            LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            RecyclerView maingridView = FindViewById<RecyclerView>(Resource.Id.gridViewmain);
            maingridView.SetLayoutManager(linearLayoutManager);
            MainGridViewAdapter = new MainGridViewAdapter(this, listItems);

            maingridView.SetAdapter(MainGridViewAdapter);

            Button button = FindViewById<Button>(Resource.Id.btnchoose);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
           

            SQLLiteDB sQLLiteDB = new SQLLiteDB();
            List<PhotoCategories> photoCategories= sQLLiteDB.GetAllCategories();

        

            

        

            SpinnerAdapter spinnerAdapter = new SpinnerAdapter(this, photoCategories);

          
            

            spinner.Adapter = spinnerAdapter;

         

            button.Click += (s,e) => {

                Intent intent = new Intent();
                intent.SetType("image/*");
                intent.PutExtra(Intent.ExtraAllowMultiple, true);
                intent.SetAction(Intent.ActionGetContent);
             
                StartActivityForResult(Intent.CreateChooser(intent, "إختيار الصور التى تريد إضافتها للاستوديو الخاص بك"), 1);
            
            };

            Button btnsave = FindViewById<Button>(Resource.Id.btnsave);
            btnsave.Click += (s,e) =>
            {
              
                
                int id = Convert.ToInt32(spinner.SelectedItem);

                if (id==0)
                {
                    View view = (View)s;
                    Snackbar.Make(view, "يجب تحديد الفئة قبل الحفظ", Snackbar.LengthIndefinite)

            .SetAction(" متابعة", (v) => {




            })
            .Show();
                }

            
                else
                {
                    progress = new ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("جار حفظ البيانات");
                    progress.SetCancelable(false);
                    progress.Show();
                    string catname = photoCategories.Find(x => x.CatID == id).CatName;
                    Task startupWork = new Task(() => { SimulateStartup(id, catname); });

                    startupWork.Start();


                    if (selectedListItems.Count > 0)
                    {

                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);

                    }
                    else
                    {
                        View view = (View)s;
                        Snackbar.Make(view, "يجب تحديد الصور قبل الحفظ", Snackbar.LengthIndefinite)

                   .SetAction(" متابعة", (v) => {




                   })
                   .Show();
                    }



                }






            };

        }
        public async void SimulateStartup(int CatID, string CatName)
        {
         

            Java.IO.File file = new Java.IO.File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"),CatName);


            if (!file.Exists())
            {
                file.Mkdirs();

                if (selectedListItems.Count > 0)
                {

                    foreach (var item in selectedListItems)
                    {

                        Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, item.PhotoPath);

                        string filepath = file.AbsolutePath + Java.IO.File.Separator + Guid.NewGuid().ToString() + ".jpg";

                        var outputStream = new FileStream(filepath, FileMode.Create);

                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outputStream);
                        outputStream.Close();


                    }
                }


            }
            else
            {
                if (selectedListItems.Count > 0)
                {


                    foreach (var item in selectedListItems)
                    {

                        Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, item.PhotoPath);


                        string filepath = file.AbsolutePath + Java.IO.File.Separator+ Guid.NewGuid().ToString() + ".jpg";


                        var outputStream = new FileStream(filepath, FileMode.Create);

                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outputStream);
                        outputStream.Close();



                    }
                }

            }
            RunOnUiThread(() =>
            {
                progress.Hide();
            });




        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && data!= null)
            {
              

                if (data.ClipData != null)
                {
                    selectedListItems = new List<SelectedGridviewDataSource>();
                    for (int i = 0; i < data.ClipData.ItemCount; i++)
                    {
                     ClipData.Item item=    data.ClipData.GetItemAt(i);

                        Android.Net.Uri uri = item.Uri;

                        selectedListItems.Add(new SelectedGridviewDataSource(uri));
                    }
                    

                }
                else
                {
                    selectedListItems = new List<SelectedGridviewDataSource>();


                        selectedListItems.Add(new SelectedGridviewDataSource(data.Data));
                  


                }

                LinearLayoutManager linearLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
                RecyclerView maingridView = FindViewById<RecyclerView>(Resource.Id.gridViewmain);
                maingridView.SetLayoutManager(linearLayoutManager);
                SelectedGridViewAdapter = new SelectedGridViewAdapter(this, selectedListItems);

                maingridView.SetAdapter(SelectedGridViewAdapter);

        


            }
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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

        public List<MainGridviewDataSource> LoadAllImagesByCatID(int CatID)
        {
            List<MainGridviewDataSource> mainGridviewDataSources = new List<MainGridviewDataSource>();
            SQLLiteDB sQLLiteDB = new SQLLiteDB();
            List<PhotoCategories> photoCategories = sQLLiteDB.GetAllCategories();
            Java.IO.File file = new Java.IO.File(Application.Context.GetExternalFilesDir("ستوديو_حياتى"), photoCategories.Find(x => x.CatID == CatID).CatName);


            if (file.Exists())
            {
                Java.IO.File[] files = file.ListFiles();
                foreach (Java.IO.File f in files)
                {
                    String absolutePath = f.AbsolutePath;
                    String extension = absolutePath.Substring(absolutePath.LastIndexOf("."));
                  
                        mainGridviewDataSources.Add(new MainGridviewDataSource(Android.Net.Uri.FromFile(f),Drawable.CreateFromPath(f.AbsolutePath)));
                  
                }
            }

            return mainGridviewDataSources;
        }


    }
}