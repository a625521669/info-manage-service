using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class News
    {
        public int ID { set; get; }
        public string Type { set; get; }
        public string Title { set; get; }
        public DateTime PubDate { set; get; }
        public string Detail { set; get; }
        public string Author { set; get; }
    }
}