using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Files
    {
        public int ID { set; get; }
        public string Type { set; get; }
        public string Name { set; get; }
        public string Signature { set; get; }
        public string PageCount { set; get; }
        public bool HasAttacher { set; get; }

        public string Url { set; get; }
        public DateTime PubDate { set; get; }
    }
}