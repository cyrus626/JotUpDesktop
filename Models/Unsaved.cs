using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JotUp.Models.Unsaved
{
    //Ignore this also.
    //It is intented for temporal storage in case power failure.
    //Users can still get access to this but it gets deleted 
    //after it has been saved.
    static class Unsaved
    {
        public static string Title { get; set; }
        public static string Content { get; set; }
        public static string Category { get; set; }
        public static DateTime LastUsageTime { get; set; }
    }
}
