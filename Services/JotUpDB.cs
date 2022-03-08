using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JotUp.Models;
using LiteDB;
namespace JotUp.Services
{
    class JotUpDB
    {
        static string DBPath { get; set; } = "JotUp.db";
        static LiteDatabase JotUpLiteDB { get; } = new LiteDatabase(DBPath);
        public static JotPage StoreJottings(JotPage pageData)
        {
            var collection = JotUpLiteDB.GetCollection<JotPage>(nameof(JotPage));
            collection.Upsert(pageData);
            return pageData;
        }
        public static bool DeleteJottings(int pageId)
        {
            var collection = JotUpLiteDB.GetCollection<JotPage>(nameof(JotPage));
            return collection.Delete(pageId);
        }
        public static List<JotPage> GetAllJottings()
        {
            var collection = JotUpLiteDB.GetCollection<JotPage>(nameof(JotPage));
            var pages = collection.FindAll();
            return pages.ToList();
        }
        public static JotPage GetOnePageBySearch(string pageTitle)
        {
            var collection = JotUpLiteDB.GetCollection<JotPage>(nameof(JotPage));
            return collection.FindOne(page => page.Title == pageTitle);
        }
    }
}
