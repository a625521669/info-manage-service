using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Votes
    {
        public int ID { set; get; }
        public string NO { set; get; }
        public string Name { set; get; }
        public string Title { set; get; }
        public string Type { set; get; }
        public string Detail { set; get; }
        public DateTime PubDate { set; get; }
        public DateTime BeginDate { set; get; }
        public DateTime EndDate { set; get; }
    }
}