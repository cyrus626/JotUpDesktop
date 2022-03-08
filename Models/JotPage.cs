using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JotUp.Models
{
    class JotPage
    { 
        [LiteDB.BsonId(true)]
        public int PageId { get; set; } = 0;
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime LastUsageTime{ get; set; }

        
    }
}
