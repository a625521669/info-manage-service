using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Awards
    {
        public int ID { set; get; }
        public string Type { set; get; }
        public string NO { set; get; }
        public DateTime PubDate { set; get; }
        public string Detail { set; get; }
        public bool HasAttacher { set; get; }
    }
}