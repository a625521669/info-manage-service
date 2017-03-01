using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class DepGroup
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public int OwnDep { set; get; }
        public string Leader { set; get; }
        public string Member { set; get; }
        public string Detail { set; get; }
        public DateTime PubDate { set; get; }
    }
}