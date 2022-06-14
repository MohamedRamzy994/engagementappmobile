using SQLite;
using System.Collections.Generic;
using System.IO;



namespace EngagementApp.DB
{
    public class SQLLiteDB
    {

        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EngagmentApp.db3");


        public SQLLiteDB()
        {
           
            if (!File.Exists(dbPath))
            {

                var db = new SQLiteConnection(dbPath);
                db.CreateTable<PhotoCategories>();

            }
          

        }

        public void InsertCategories()
        {
            var db = new SQLiteConnection(dbPath);
            List<PhotoCategories> photoCategories = new List<PhotoCategories>();

            photoCategories.Add(new PhotoCategories("صور قرأة الفاتحة"));
            photoCategories.Add(new PhotoCategories("صور الخطوبة"));
            photoCategories.Add(new PhotoCategories("صور كتب الكتاب"));
            photoCategories.Add(new PhotoCategories("صور  الفرح"));
            photoCategories.Add(new PhotoCategories("صور  شهر العسل"));
            photoCategories.Add(new PhotoCategories("صور  الحمل والولادة"));

            foreach (var item in photoCategories)
            {
                db.Insert(item);
            }

        }

        public List<PhotoCategories> GetAllCategories ()
        {
            List<PhotoCategories> photoCategories = new List<PhotoCategories>();
            var db = new SQLiteConnection(dbPath);
            TableQuery<PhotoCategories> categories = db.Table<PhotoCategories>();
            foreach (var item in categories)
            {

                photoCategories.Add(item);
            }

            return photoCategories;

        }


    }
    [Table("PhotoCategories")]
    public class PhotoCategories
    {
        [PrimaryKey,AutoIncrement,Column("CatID")]
        public int CatID { get; set; }
        [Column("CatName")]
        public string CatName { get; set; }

        public PhotoCategories()
        {
                
        }
        public PhotoCategories(string CateName)
        {
            this.CatName = CateName;
        }


    }
}